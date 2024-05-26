﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
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
                });
            //.AddJwtBearer(opts =>
            //{
            //    opts.RequireHttpsMetadata = false;
            //    opts.SaveToken = true;
            //    opts.MapInboundClaims = false;
            //    opts.TokenValidationParameters.RoleClaimType = "role";
            //    opts.TokenValidationParameters.NameClaimType = "name";

            //    opts.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey =
            //       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Security:Jwt:Secret").Value!)),
            //        ClockSkew = TimeSpan.Zero,
            //        RoleClaimType = ClaimTypes.Role
            //    };
            //});

            builder.Services.AddAuthorization();
        }
    }
}
