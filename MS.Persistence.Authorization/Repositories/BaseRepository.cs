using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Common;
using MS.Persistence.Authorization.Data;
using System.Linq.Expressions;

namespace MS.Persistence.Authorization.Repositories
{
    public class BaseRepository<T>(AuthorizationDbContext context) : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly AuthorizationDbContext Context = context;

        public async Task CreateAsync(T entity)
        {
            await Context.AddAsync(entity);
        }

        public async Task<bool> EntityExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().AsNoTracking().Where(predicate).AnyAsync(cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
        }
    }
}
