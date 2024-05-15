using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Authorization.Repositories;
using MS.Persistence.Authorization.Data;
using MS.Persistence.Authorization.Repositories;

namespace MS.Persistence.Authorization
{
    public static class DependencyInjection
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthorizationConnection");
            services.AddDbContext<AuthorizationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
