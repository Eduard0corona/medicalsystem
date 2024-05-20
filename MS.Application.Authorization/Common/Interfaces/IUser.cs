using System.Security.Claims;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface IUser
    {
        string? Id { get; }
        ClaimsIdentity? ClaimsIdentity { get; }
    }
}
