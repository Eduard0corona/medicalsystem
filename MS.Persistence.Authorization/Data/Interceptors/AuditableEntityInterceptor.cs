using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MS.Application.Authorization.Common.Interfaces;
using MS.Domain.Authorization.Common;
using MS.Persistence.Authorization.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Persistence.Authorization.Data.Interceptors
{
    public class AuditableEntityInterceptor(IUser user) : SaveChangesInterceptor
    {
        private readonly IUser _user = user;
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var utcNow = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = _user.Id!;
                        entry.Entity.DateCreated = utcNow;
                    }
                    entry.Entity.ModifiedBy = _user.Id;
                    entry.Entity.DateModified = utcNow;
                }
            }
        }
    }
}
