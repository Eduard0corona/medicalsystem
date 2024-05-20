using MediatR;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.CreateRole
{
    public sealed record CreateRoleRequest(string Name, string Description) : IRequest<Result<CreateRoleResponse>>;
}
