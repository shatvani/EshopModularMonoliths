using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors;
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {

        UpdateEntities(eventData.Context);

        var context = eventData.Context;
        if (context == null)
            return base.SavingChanges(eventData, result);
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity && (e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified));
        var currentTime = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            var auditableEntity = (IAuditableEntity)entry.Entity;
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                auditableEntity.CreatedAt = currentTime;
            }
            auditableEntity.LastModified = currentTime;
        }
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);


        var context = eventData.Context;
        if (context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity && (e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified));
        var currentTime = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            var auditableEntity = (IAuditableEntity)entry.Entity;
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                auditableEntity.CreatedAt = currentTime;
            }
            auditableEntity.LastModified = currentTime;
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<IAuditableEntity>();
        var currentTime = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            var isAdded = entry.State == EntityState.Added;
            var isModifiedOrOwnedChanged = entry.State == EntityState.Modified || entry.HasChangedOwnedEntities();

            if (isAdded)
            {
                entry.Entity.CreatedBy = "mehmet";
                entry.Entity.CreatedAt = currentTime;
            }

            if (isAdded || isModifiedOrOwnedChanged)
            {
                entry.Entity.LastModifiedBy = "mehmet";
                entry.Entity.LastModified = currentTime;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
