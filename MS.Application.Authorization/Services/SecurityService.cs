using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MS.Application.Authorization.Common.Interfaces;
using MS.Domain.Authorization.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MS.Application.Authorization.Services
{
    public class SecurityService(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor) : ISecurityService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(User userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Security:Jwt:Secret").Value!);

            var claims = new List<Claim>()
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, userInfo.Email),
                    new(JwtRegisteredClaimNames.Email, userInfo.Email),
                    new(JwtRegisteredClaimNames.Name, userInfo.Email),

                    new("userid", userInfo.Id.ToString()),
                    new("username", userInfo.Username)
                };

            if (userInfo.UserRoles != null)
            {
                foreach (var role in userInfo.UserRoles)
                {
                    if (role.Role != null)
                    {
                        claims.Add(new(ClaimTypes.Role, role.Role!.Name));
                    }
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration.GetSection("Security:Jwt:ExpireHours").Value!)),
                Issuer = "https://localhost:7191/",
                Audience = "https://localhost:7191/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Security:Jwt:Secret").Value!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;

                //retrieve claims by key.
                //var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "userid").Value);

                // Create a ClaimsIdentity and ClaimsPrincipal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return claimsPrincipal; ;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string HashPassword(string password)
        {
            string salt = _configuration.GetSection("Security:Salt").Value!;
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            //Validar si no se necesita mandar de esta forma en los unit test
            //string salt = _configuration.GetSection("Security:Salt").Value!;
            //string passwordSalted = string.Concat(password,salt);

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public RefreshToken CreateRefreshToken(Guid userId)
        {
            return new RefreshToken
            {
                Token = GenerateRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                DateCreated = DateTime.UtcNow,
                UserId = userId
            };
        }

        public void SetTokenCookies(string accessToken, string refreshToken)
        {
            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration.GetSection("Security:Jwt:ExpireHours").Value!))
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
