using MediatR;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole
{
    public sealed record GetUserRoleRequest(Guid userId) : IRequest<List<GetUserRoleResponse>> { }
}
