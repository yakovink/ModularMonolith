 
using System.Collections;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Exceptions;

namespace Shared.Data;

public abstract class GenericCachedRepository<Model, Context>(GenericRepository<Model, Context> repository, IDistributedCache cache) : IGenericRepository<Model>
where Model : class, IEntity<Guid>
where Context : DbContext
{

    public async Task<int> SaveChangesAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var result = await repository.SaveChangesAsync(Id, cancellationToken);
        await removeCache(Id.ToString(), cancellationToken);
        return result;
    }

    protected async Task saveCache(string key, Model value, CancellationToken cancellationToken)
    {
        Console.WriteLine(JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = false
        }));
        await cache.SetStringAsync(key.ToString(), JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = false
        }), cancellationToken);
    }

    protected async Task saveOrReplace(string key, Model value, CancellationToken cancellationToken)
    {
        if (await inCache(key, cancellationToken))
        {
            await removeCache(key, cancellationToken);
        }
        await saveCache(key, value, cancellationToken);
    }

    protected async Task<T> loadCache<T>(string key, CancellationToken cancellationToken)
    where T : class
    {
        string obj = await cache.GetStringAsync(key, cancellationToken) ?? throw new NotFoundException($"cache for {key} was not found");
        Console.WriteLine(obj);
        T result = JsonSerializer.Deserialize<T>(obj) ?? throw new NotFoundException($"cache value is not valid for {typeof(T)}");
        return result;

    }

    protected async Task removeCache(string key, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    protected async Task<bool> inCache(string Id, CancellationToken cancellationToken)
    {
        return (await cache.GetAsync(Id, cancellationToken)) != null;
    }

    public async Task<Model> GetElementById(Guid Id, bool AsNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Model, object>>[] includes)
    {
        Model modelItem;
        if (!AsNoTracking)
        {
            return await repository.GetElementById(Id, AsNoTracking, cancellationToken, includes);
        }
        try
        {
            return await loadCache<Model>(Id.ToString(), cancellationToken);
        }
        catch (NotFoundException)
        {
            modelItem = await repository.GetElementById(Id, AsNoTracking, cancellationToken, includes);
            await SaveChangesAsync(Id, cancellationToken);
            return modelItem;
        }

    }

    public async Task<IEnumerable<Model>> GetElements(Expression<Func<Model, bool>> predicate = default!, bool AsNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Model, object>>[] includes)
    {
        IEnumerable<Model> models = await repository.GetElements(predicate, AsNoTracking, cancellationToken, includes);
        foreach (Model mod in models)
        {
            await saveOrReplace(mod.Id.ToString(), mod, cancellationToken);
        }
        return models;
    }

    public async Task<Model> CreateElement(Model Element, CancellationToken cancellationToken = default)
    {
        await repository.CreateElement(Element, cancellationToken);
        await saveCache(Element.Id.ToString(), Element, cancellationToken);
        return Element;
    }

    public async Task<bool> DeleteElement(Guid id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteElement(id, cancellationToken);
        await removeCache(id.ToString(), cancellationToken);
        return true;
    }

    public async Task<bool> ReloadElementCollection<TProperty>(Model Element, Expression<Func<Model, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
    where TProperty : class, IEntity<Guid>

    {

        await repository.ReloadElementCollection(Element, collectionProperty, cancellationToken);
        await saveCache(Element.Id.ToString(), Element, cancellationToken);

        return true;
    }

    public async Task<TProperty> AddElementToCollection<TProperty>(TProperty property, Expression<Func<TProperty,Guid>> foreignKey, Expression<Func<TProperty, bool>> condition , Func<TProperty,object> actionIfExist, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>
    {
        TProperty i = await repository.AddElementToCollection(property,foreignKey,condition,actionIfExist, cancellationToken);
        Guid ModelId = foreignKey.Compile().Invoke(i);
        await removeCache(ModelId.ToString(), cancellationToken);
        Model mod = await GetElementById(ModelId, true, cancellationToken);
        await saveCache(ModelId.ToString(), mod, cancellationToken);
        return i;
    }
    public async Task<bool> RemoveElementFromCollection<TProperty>(TProperty property, Expression<Func<TProperty,Guid>> foreignKey, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>
    {
        bool result = await repository.RemoveElementFromCollection(property, foreignKey, cancellationToken);
        Guid ModelId = foreignKey.Compile().Invoke(property);
        await removeCache(ModelId.ToString(), cancellationToken);
        Model mod = await GetElementById(ModelId, true, cancellationToken);
        await saveCache(ModelId.ToString(), mod, cancellationToken);
        return result;
    }

    public Task<bool> CheckIfItemExist<TProperty>(TProperty property, Expression<Func<TProperty, bool>> condition, CancellationToken cancellationToken) where TProperty : class , IEntity<Guid>
    {
        return repository.CheckIfItemExist(property, condition, cancellationToken);
    }
}
