using MediatR;
using MS.Application.Authorization.Common.Security;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser
{
    [Authorize(Roles = "Admin")]
    public sealed record GetAllUserRequest : IRequest<List<GetAllUserResponse>>;
}
