using MediatR;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public sealed record CreateUserRequest(string UserName, string Email, string Password) : IRequest<CreateUserResponse>;
}
