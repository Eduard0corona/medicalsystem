using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;
using MS.Persistence.Authorization.Data;
using MS.Persistence.Authorization.Data.Interceptors;
using MS.Persistence.Authorization.Repositories;

namespace MS.Persistence.Authorization
{
    public static class DependencyInjection
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            var connectionString = configuration.GetConnectionString("AuthorizationConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<AuthorizationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IAuthorizationDbContext, AuthorizationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

        }
    }
}
