using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Common;
using MS.Domain.Authorization.Entities;
using MS.Persistence.Authorization.Data;

namespace MS.Persistence.Authorization.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AuthorizationDbContext context) : base(context) { }

        public async Task<User> GetByUsername(string username, CancellationToken cancellationToken)
        {
            var user =  await Context.Users.Where(s=> s.Username == username).FirstOrDefaultAsync();
            return user!;
        }
    }
}
