using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserHandler(ISecurityService securityService, IAuthorizationDbContext authorizationDbContext, IConfiguration configuration) : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
    {
        readonly ISecurityService _securityService = securityService;
        readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;
        readonly IConfiguration _configuration = configuration;

        public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var userToken = new LoginUserResponse();

            var userInfo = await _authorizationDbContext.Users
                .Where(s=> s.Username == request.Username)
                .Include(s => s.UserRoles!)
                .ThenInclude(s => s.Role)
                .FirstOrDefaultAsync(cancellationToken);

            if(userInfo is null) 
            {
                return Result<LoginUserResponse>.Failure("User credentials are wrong.");
            }

            var isValidPassword = _securityService.VerifyPassword(request.Password, userInfo.PasswordHash);

            if (!isValidPassword)
            {
                return Result<LoginUserResponse>.Failure("User credentials are wrong.");   
            }

            var newAccessToken = _securityService.GenerateToken(userInfo);
            var newRefreshToken = _securityService.CreateRefreshToken(userInfo.Id);

            userToken.RefreshToken = newRefreshToken.Token;
            userToken.Token = newAccessToken;

            var tokenEntity = new Token() { UserId = userInfo.Id, Value = newAccessToken, ExpiryDate = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration.GetSection("Security:Jwt:ExpireHours").Value!)) };

            await _authorizationDbContext.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);
            await _authorizationDbContext.Tokens.AddAsync(tokenEntity, cancellationToken);

            await _authorizationDbContext.SaveChangesAsync(cancellationToken);

            _securityService.SetTokenCookies(newAccessToken, newRefreshToken.Token);

            return Result<LoginUserResponse>.Success(userToken);
        }
    }
}
