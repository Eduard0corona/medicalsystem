using MS.Domain.Authorization.Common;

namespace MS.Domain.Authorization.Entities
{
    public class Token : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
    }
}
