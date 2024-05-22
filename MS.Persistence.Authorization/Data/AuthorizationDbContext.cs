using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using MS.Domain.Authorization.Common;
using MS.Domain.Authorization.Entities;
using System.Reflection;

namespace MS.Persistence.Authorization.Data
{
    public class AuthorizationDbContext : DbContext, IAuthorizationDbContext
    {
        public AuthorizationDbContext() { }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Token> Tokens => Set<Token>();

        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
