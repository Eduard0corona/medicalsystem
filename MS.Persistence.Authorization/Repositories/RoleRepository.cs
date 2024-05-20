using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Entities;
using MS.Persistence.Authorization.Data;

namespace MS.Persistence.Authorization.Repositories
{
    public class RoleRepository(AuthorizationDbContext context) : BaseRepository<Role>(context), IRoleRepository
    {
    }
}
