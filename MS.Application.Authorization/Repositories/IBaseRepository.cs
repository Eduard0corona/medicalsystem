using MS.Domain.Authorization.Common;
using System.Linq.Expressions;

namespace MS.Application.Authorization.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> EntityExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
