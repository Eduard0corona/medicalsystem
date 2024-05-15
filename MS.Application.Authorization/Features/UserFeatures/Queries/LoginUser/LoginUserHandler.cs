using MediatR;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserHandler(IUserRepository userRepository, ISecurityService securityService) : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly ISecurityService _securityService = securityService;

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var userToken = new LoginUserResponse();

            var userInfo = await _userRepository.GetByUsername(request.Username, cancellationToken);

            if (userInfo != null)
            {
                var isValidPassword = _securityService.VerifyPassword(request.Password, userInfo.PasswordHash);

                if (isValidPassword)
                {
                    userToken.Token = _securityService.GenerateToken(userInfo);
                }
            }

            return userToken;
        }
    }
}
