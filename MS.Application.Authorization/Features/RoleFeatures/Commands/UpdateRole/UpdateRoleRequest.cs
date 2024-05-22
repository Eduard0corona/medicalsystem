using MediatR;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.UpdateRole
{
    public sealed record UpdateRoleRequest(int Id,string Name, string Description) : IRequest<Result<UpdateRoleResponse>>;
}
