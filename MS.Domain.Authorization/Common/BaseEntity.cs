namespace MS.Domain.Authorization.Common
{
    public abstract class BaseEntity<T> : IEntity
    {
        public virtual T? Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; }
    }
}
