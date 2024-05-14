using Microsoft.EntityFrameworkCore;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface IAuthorizationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Token> Tokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
