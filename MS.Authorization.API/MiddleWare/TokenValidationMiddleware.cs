using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MS.Authorization.API.MiddleWare
{
    public class TokenValidationMiddleware(RequestDelegate next, ILogger<TokenValidationMiddleware> logger, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<TokenValidationMiddleware> _logger = logger;
        private readonly string _key = configuration.GetSection("Security:Jwt:Key").Value!;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing");
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token missing");
                return;
            }

            var userInfo = ValidateToken(token);

            if (userInfo == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            // Attach user information to the context
            context.Items["User"] = userInfo;

            await _next(context);
        }

        private object ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Additional validation parameters can be set here
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;
                var userName = jwtToken.Claims.First(claim => claim.Type == "name").Value;

                return new { UserId = userId, UserName = userName };
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError($"Token validation failed: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
