using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsername(string username, CancellationToken cancellationToken);
    }
}
