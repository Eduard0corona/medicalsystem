namespace MS.Authorization.API.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetTokenCookies(string accessToken, string refreshToken)
        {
            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                httpContext.Response.Cookies.Append("access_token", accessToken, accessTokenCookieOptions);
                httpContext.Response.Cookies.Append("refresh_token", refreshToken, refreshTokenCookieOptions);
            }
        }
    }
}
