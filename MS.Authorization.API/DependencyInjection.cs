using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.Application.Authorization.Common.Interfaces;
using MS.Authorization.API.Services;
using System.Text;

namespace MS.Authorization.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services) 
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            return services;
        }

        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts => opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Security:Jwt:Secret").Value!)),
                    ClockSkew = TimeSpan.Zero
            });

            builder.Services.AddAuthorization();
        }
    }
}
