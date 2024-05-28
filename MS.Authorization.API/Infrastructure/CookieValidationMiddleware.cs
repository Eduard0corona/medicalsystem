using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MS.Application.Authorization.Common.Interfaces;
using MS.Domain.Authorization.Entities;
using System.Security.Claims;

namespace MS.Authorization.API.Infrastructure
{
    public class CookieValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, ISecurityService securityService)
        {
            var path = context.Request.Path;
            if (path.StartsWithSegments("/.well-known"))
            {
                // Skipping well-known endpoint
                await _next(context);
                return;
            }

            if (path.StartsWithSegments("/api/auth"))
            {
                // Skipping well-known endpoint
                await _next(context);
                return;
            }

            var accessToken = context.Request.Cookies["access_token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Request.Headers.Append("Authorization", $"Bearer {accessToken}");
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
