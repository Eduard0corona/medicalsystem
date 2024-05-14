using MS.Domain.Authorization.Common;

namespace MS.Domain.Authorization.Entities
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IReadOnlyList<UserRole>? UserRoles { get; set; }
    }
}
