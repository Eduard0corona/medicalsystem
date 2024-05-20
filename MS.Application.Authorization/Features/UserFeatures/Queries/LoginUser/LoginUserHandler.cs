using MediatR;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserHandler(IUserRepository userRepository, ISecurityService securityService) : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly ISecurityService _securityService = securityService;

        public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var userToken = new LoginUserResponse();

            var userInfo = await _userRepository.GetByUsername(request.Username, cancellationToken);

            if(userInfo is null) 
            {
                return Result<LoginUserResponse>.Failure("User credentials are wrong.");
            }

            var isValidPassword = _securityService.VerifyPassword(request.Password, userInfo.PasswordHash);

            if (!isValidPassword)
            {
                return Result<LoginUserResponse>.Failure("User credentials are wrong.");   
            }

            userToken.Token = _securityService.GenerateToken(userInfo);

            return Result<LoginUserResponse>.Success(userToken);
        }
    }
}
