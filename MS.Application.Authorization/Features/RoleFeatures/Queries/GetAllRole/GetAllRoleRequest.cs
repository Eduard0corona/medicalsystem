using MediatR;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public sealed record GetAllRoleRequest : IRequest<List<GetAllRoleResponse>>{}
}
