using MediatR;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken
{
    public sealed record UpdateRefreshTokenRequest(string RefreshToken) : IRequest<Result<UpdateRefreshTokenResponse>>;
}
