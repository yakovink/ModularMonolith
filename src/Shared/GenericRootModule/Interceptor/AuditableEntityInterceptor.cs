


using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DDD;

namespace Shared.GenericRootModule.Interceptor;

public class AuditableEntityInterceptor:SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }


    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken); 

    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity._createdDate=DateTime.UtcNow;
                entry.Entity._createdBy=Environment.UserName;
            }

            if (entry.State == EntityState.Modified|| entry.State == EntityState.Added || entry.HasChangedOwnedEntities()){
                entry.Entity._lastModifiedDate=DateTime.UtcNow;
                entry.Entity._lastModifiedBy=Environment.UserName;
            }

        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r => 
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
