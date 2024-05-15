using MediatR;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public sealed record LoginUserRequest(string Username, string Password) : IRequest<LoginUserResponse>;
}
