using MS.Domain.Authorization.Common;

namespace MS.Domain.Authorization.Entities
{
    public class UserRole : IEntity
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
