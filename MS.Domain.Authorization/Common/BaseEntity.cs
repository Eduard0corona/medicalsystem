namespace MS.Domain.Authorization.Common
{
    public abstract class BaseEntity<T> : IEntity
    {
        public virtual T? Id { get; set; }
    }
}
