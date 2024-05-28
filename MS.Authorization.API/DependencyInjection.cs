using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Application.Authorization.Common.Interfaces;
using MS.Authorization.API.Infrastructure;
using MS.Authorization.API.Services;
using System.Security.Claims;
using System.Text;

namespace MS.Authorization.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services) 
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            //services.AddExceptionHandler<CustomExceptionHandler>();

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
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.Name = "MedicalSystem";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                //opts.Authority = "https://localhost:7191/";
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.MapInboundClaims = true;
                opts.TokenValidationParameters.RoleClaimType = "role";
                opts.TokenValidationParameters.NameClaimType = "name";

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Security:Jwt:Secret").Value!)),
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role
                };

                opts.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var path = context.Request.Path;
                        if (path.StartsWithSegments("/.well-known"))
                        {
                            return Task.CompletedTask;
                        }

                        var accessToken = context.Request.Cookies["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();
        }
    }
}
