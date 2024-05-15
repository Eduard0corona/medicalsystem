using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Common;
using MS.Persistence.Authorization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MS.Persistence.Authorization.Repositories
{
    public class BaseRepository<T>(AuthorizationDbContext context) : IBaseRepository<T> where T : BaseEntity<T>
    {
        protected readonly AuthorizationDbContext Context = context;

        public async Task CreateAsync(T entity)
        {
            await Context.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            var existingEntity = Context.Set<T>().FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                Context.Entry(existingEntity).State = EntityState.Modified;
                Context.Update(existingEntity);
            }
        }

        public async Task<bool> EntityExistsAsync(T entity)
        {
            return await Context.Set<T>().AnyAsync(o => o.Id == entity.Id);
        }

        public async Task<T> Get(T entity, CancellationToken cancellationToken)
        {
            var result = await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            return result!;
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
