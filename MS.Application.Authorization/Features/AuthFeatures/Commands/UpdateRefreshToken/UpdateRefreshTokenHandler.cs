using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Application.Authorization.Features.RoleFeatures.Commands.UpdateRole;

namespace MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenHandler(IAuthorizationDbContext authorizationDbContext, ISecurityService securityService, IMapper mapper) : IRequestHandler<UpdateRefreshTokenRequest, Result<UpdateRefreshTokenResponse>>
    {
        readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;
        readonly ISecurityService _securityService = securityService;
        readonly IMapper _mapper = mapper;

        public async Task<Result<UpdateRefreshTokenResponse>> Handle(UpdateRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var entity = await _authorizationDbContext.RefreshTokens
                .FirstOrDefaultAsync(s => s.Token == request.RefreshToken, cancellationToken);

            if (entity == null || !entity.IsActive)
            {
                return Result<UpdateRefreshTokenResponse>.Failure("Invalid refresh token.");
            }

            var user = await _authorizationDbContext.Users.FindAsync([entity.UserId, cancellationToken], cancellationToken: cancellationToken);
            if (user == null)
            {
                return Result<UpdateRefreshTokenResponse>.Failure("Invalid user.");

            }

            var newAccessToken = _securityService.GenerateToken(user);
            var newRefreshToken = _securityService.CreateRefreshToken(user.Id);

            entity.Revoked = DateTime.UtcNow;
            _authorizationDbContext.RefreshTokens.Update(entity);
            _authorizationDbContext.RefreshTokens.Add(newRefreshToken);
            await _authorizationDbContext.SaveChangesAsync(cancellationToken);

            _securityService.SetTokenCookies(newAccessToken, newRefreshToken.Token);
            return Result<UpdateRefreshTokenResponse>.Success(_mapper.Map<UpdateRefreshTokenResponse>(entity));
        }
    }
}
