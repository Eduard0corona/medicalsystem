namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole
{
    public sealed record GetUserRoleResponse
    {
        public Guid UserId { get; set; }
        public IReadOnlyList<string> Roles { get; set; } = null!;
    }
}
