using MS.Application.Authorization.Common.Interfaces;
using System.Security.Claims;

namespace MS.Authorization.API.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsIdentity? ClaimsIdentity => _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
    }
}
