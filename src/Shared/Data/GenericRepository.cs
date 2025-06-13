using System;
using System.Linq.Expressions;
using Shared.Exceptions;

namespace Shared.Data;

public abstract class GenericRepository<Model, Context>(GenericDbContext<Context> dbContext) : IGenericRepository<Model>
    where Model : class, IEntity<Guid>
    where Context : DbContext
{
    public async Task<Model> CreateElement(Model Element, CancellationToken cancellationToken = default)
    {
        await dbContext.GetDbSet<Model>().AddAsync(Element, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Element;
    }

    public async Task<bool> DeleteElement(Guid id, CancellationToken cancellationToken = default)
    {
        var Element = await GetElementById(id, false, cancellationToken);
        dbContext.GetDbSet<Model>().Remove(Element);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Model> GetElementById(Guid Id, bool AsNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Model, object>>[] includes)
    {
        DbSet<Model> set = dbContext.GetDbSet<Model>();
        IQueryable<Model> query = set;

        if (AsNoTracking)
        {
            query = query.AsNoTracking();
        }
        foreach (var include in includes)
        {

            query = query.Include(include);

        }
        Model? entity = await query.SingleOrDefaultAsync(e => e.Id.Equals(Id), cancellationToken);
        return entity ?? throw new NotFoundException($"Element not found {Id}");

    }

    public async Task<IEnumerable<Model>> GetElements(Expression<Func<Model, bool>> predicate = default!, bool AsNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<Model, object>>[] includes)

    {
        DbSet<Model> set = dbContext.GetDbSet<Model>();
        IQueryable<Model> query = set;

        if (AsNoTracking)
        {
            query = query.AsNoTracking();
        }
        if (predicate != null)
        {

            query = query.Where(predicate);
            
        }
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync(cancellationToken);
    }



    public async Task<int> SaveChangesAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);

    }

    public async Task<bool> ReloadElementCollection<TProperty>(Model Element, Expression<Func<Model, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
    where TProperty : class, IEntity<Guid>
    {
        await dbContext.Entry(Element).Collection(collectionProperty).LoadAsync(cancellationToken);
        return true;
    }
    
    public async Task<TProperty> AddElementToCollection<TProperty>(TProperty property, Expression<Func<TProperty,Guid>> foreignKey, Expression<Func<TProperty, bool>> condition , Func<TProperty,object> actionIfExist, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>
    {
        var set = dbContext.GetDbSet<TProperty>();

        if (await CheckIfItemExist(property, condition, cancellationToken))
        {
            property = await set.Where(condition).SingleAsync(cancellationToken);
            actionIfExist(property);
        }
        else
        {
            await set.AddAsync(property, cancellationToken);
        }
        Model element = await GetElementById(foreignKey.Compile()(property), false, cancellationToken);
        await SaveChangesAsync(element.Id, cancellationToken);
        
        return property;
    }
    public async Task<bool> RemoveElementFromCollection<TProperty>(TProperty property, Expression<Func<TProperty,Guid>> foreignKey, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>
    {
        dbContext.GetDbSet<TProperty>().Remove(property);
        await SaveChangesAsync(foreignKey.Compile()(property),cancellationToken);
        return true;
    }

    public async Task<bool> CheckIfItemExist<TProperty>(TProperty property, Expression<Func<TProperty, bool>> condition, CancellationToken cancellationToken) where TProperty : class, IEntity<Guid>
    {
        return await dbContext.GetDbSet<TProperty>().Where(condition).CountAsync() == 1;
        
    }


}
