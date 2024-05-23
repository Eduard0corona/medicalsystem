﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserHandler(ISecurityService securityService, IAuthorizationDbContext authorizationDbContext) : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
    {
        readonly ISecurityService _securityService = securityService;
        readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;

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

            var refreshToken = await GetRefreshTokenAsync(userInfo, cancellationToken);

            if (refreshToken is null)
            {
                return Result<LoginUserResponse>.Failure("Failed to create a refresh token.");
            }


            userToken.RefreshToken = refreshToken.Token;
            userToken.Token = _securityService.GenerateToken(userInfo);

            return Result<LoginUserResponse>.Success(userToken);
        }

        private async Task<RefreshToken> GetRefreshTokenAsync(User userInfo, CancellationToken cancellationToken)
        {
            var refreshToken = _securityService.CreateRefreshToken(userInfo.Id);
            await _authorizationDbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _authorizationDbContext.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }
    }
}
