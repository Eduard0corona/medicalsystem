namespace MS.Domain.Authorization.Common
{
    public interface IEntity 
    {
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        string CreatedBy { get; set; }
        string? ModifiedBy { get; set; }
    }
}
