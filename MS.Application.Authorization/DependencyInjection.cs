using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Authorization.Common.Behaviours;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Services;
using System.Reflection;

namespace MS.Application.Authorization
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<IJwtService, JwtService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            
            return services;
        }
    }
}
