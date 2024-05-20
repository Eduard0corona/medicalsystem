using System.Security.Claims;

namespace MS.Application.Authorization.Common.Interfaces
{
    public interface IUser
    {
        public ClaimsIdentity? ClaimsIdentity { get; }
    }
}
