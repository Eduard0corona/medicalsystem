using MS.Application.Authorization.Common.Interfaces;

namespace MS.Authorization.API.Infrastructure
{
    public class CookieValidationMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        private readonly RequestDelegate _next = next;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task InvokeAsync(HttpContext context, ISecurityService securityService)
        {
            var accessToken = _httpContextAccessor.HttpContext!.Request.Cookies["access_token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                var userId = securityService.ValidateJwtToken(accessToken);
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
