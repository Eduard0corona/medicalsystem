using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User userInfo);
    }
}
