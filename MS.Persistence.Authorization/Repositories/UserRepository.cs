using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Entities;
using MS.Persistence.Authorization.Data;

namespace MS.Persistence.Authorization.Repositories
{
    public class UserRepository(AuthorizationDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User> GetByUsername(string username, CancellationToken cancellationToken)
        {
            var user =  await Context.Users.Where(s=> s.Username == username).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return user!;
        }
    }
}
