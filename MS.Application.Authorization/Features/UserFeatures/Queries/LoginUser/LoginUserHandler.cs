using MediatR;
using Microsoft.Extensions.Configuration;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserHandler(IUserRepository userRepository, IConfiguration configuration, IJwtService jwtService) : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly IConfiguration _configuration = configuration;
        readonly IJwtService _jwtService = jwtService;

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var userToken = new LoginUserResponse();

            var userInfo = await _userRepository.GetByUsername(request.Username, cancellationToken);

            if (userInfo != null)
            {
                var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, userInfo.PasswordHash);

                if (isValidPassword)
                {
                    userToken.Token = _jwtService.GenerateToken(userInfo);
                }
            }

            return userToken;
        }
    }
}
