using MediatR;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole
{
    public sealed record GetUserRoleRequest(Guid UserId) : IRequest<List<GetUserRoleResponse>> { }
}
