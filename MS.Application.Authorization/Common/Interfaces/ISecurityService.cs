using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface ISecurityService
    {
        string GenerateToken(User userInfo);
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hashedPassword);
    }
}
