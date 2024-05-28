using MS.Domain.Authorization.Entities;
using System.Security.Claims;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface ISecurityService
    {
        string GenerateToken(User userInfo);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        ClaimsPrincipal? ValidateJwtToken(string? token);
        RefreshToken CreateRefreshToken(Guid userId);
        void SetTokenCookies(string accessToken, string refreshToken);
    }
}
