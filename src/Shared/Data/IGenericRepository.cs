using System;
using System.Linq.Expressions;

namespace Shared.Data;

public interface IGenericRepository<T> where T : class, IEntity<Guid>
{
    public Task<T> GetElementById(Guid Id, bool AsNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

    public Task<IEnumerable<T>> GetElements(Expression<Func<T, bool>> predicate = default!,
        bool AsNoTracking = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);

    public Task<T> CreateElement(T Element, CancellationToken cancellationToken = default);
    public Task<bool> DeleteElement(Guid id, CancellationToken cancellationToken = default);

    public Task<int> SaveChangesAsync(Guid Id, CancellationToken cancellationToken = default);

    public Task<bool> ReloadElementCollection<TProperty>(T Element, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>;

    public Task<bool> CheckIfItemExist<TProperty>(TProperty property, Expression<Func<TProperty, bool>> condition, CancellationToken cancellationToken=default) where TProperty : class , IEntity<Guid>;
    public Task<TProperty> AddElementToCollection<TProperty>(TProperty property, Expression<Func<TProperty, Guid>> foreignKey, Expression<Func<TProperty, bool>> condition, Func<TProperty, object> actionIfExist, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>;
    public Task<bool> RemoveElementFromCollection<TProperty>(TProperty property, Expression<Func<TProperty,Guid>> foreignKey, CancellationToken cancellationToken = default) where TProperty : class, IEntity<Guid>;
}
