using MS.Application.Authorization.Common.Interfaces;
using System.Security.Claims;

namespace MS.Authorization.API.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue("userid");
        public ClaimsIdentity? ClaimsIdentity => _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
    }
}
