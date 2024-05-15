using MS.Domain.Authorization.Common;

namespace MS.Application.Authorization.Repositories
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> EntityExistsAsync(T entity);
        Task<T> Get(T entity, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}
