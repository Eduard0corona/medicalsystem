namespace MS.Domain.Authorization.Common
{
    public abstract class BaseEntity : IEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; }
    }
}
