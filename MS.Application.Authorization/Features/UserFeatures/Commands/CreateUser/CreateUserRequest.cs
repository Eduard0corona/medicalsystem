using MediatR;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public sealed record CreateUserRequest(string UserName, string Email, string Password) : IRequest<Result<CreateUserResponse>>;
}
