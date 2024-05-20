using MediatR;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public sealed record LoginUserRequest(string Username, string Password) : IRequest<Result<LoginUserResponse>>;
}
