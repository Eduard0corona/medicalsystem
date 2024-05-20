namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public sealed record GetAllRoleResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
