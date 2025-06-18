 
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Exceptions;
using Shared.GenericRootModule;

namespace Shared.Data;

public abstract class GenericDbContext<T> : DbContext where T : DbContext

{

    public Dictionary<Type, object> Dbsets = new();

    public GenericDbContext(DbContextOptions<T> options, Type[] models) : base(options)
    {

        foreach (var model in models)
        {
            if (!typeof(IEntity<Guid>).IsAssignableFrom(model))
            {
                throw new ArgumentException($"Type {model} does not implement {typeof(IEntity<Guid>)}");
            }
            // Use reflection to call Set<model>()

            Dbsets[model] = CreateSet(model);
        }


    }

    public object CreateSet(Type modelType)
    {
        if (!typeof(IEntity<Guid>).IsAssignableFrom(modelType))
        {
            throw new ArgumentException($"Type {modelType} does not implement {typeof(IEntity<Guid>)}");
        }

        // Use reflection to call Set<modelType>()
        MethodInfo setMethod = typeof(DbContext).GetMethod(nameof(Set), Type.EmptyTypes)!;
        MethodInfo genericSetMethod = setMethod.MakeGenericMethod(modelType);
        object dbSet = genericSetMethod.Invoke(this, null)!;
        return dbSet!;
    }


    public DbSet<Model> GetDbSet<Model>() where Model : class, IEntity<Guid>
    {
        if (Dbsets.TryGetValue(typeof(Model), out var dbSet) && dbSet is DbSet<Model> typedDbSet)
        {
            return typedDbSet;
        }
        throw new InvalidOperationException($"DbSet for type {typeof(Model).Name} not found.");
    }







}
