using MS.Domain.Authorization.Common;

namespace MS.Domain.Authorization.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public IReadOnlyList<UserRole>? UserRoles { get; set; }
        public IReadOnlyList<Token>? Tokens { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
