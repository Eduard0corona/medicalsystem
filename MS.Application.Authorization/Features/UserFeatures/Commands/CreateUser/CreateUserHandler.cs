using MediatR;
using Microsoft.Extensions.Configuration;
using MS.Application.Authorization.Common.Exceptions;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepository, ISecurityService securityService) : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly ISecurityService _securityService = securityService;

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetBy(s => s.Username == request.UserName, cancellationToken);

            if(userExist.Count > 0) 
            {
                throw new UserAlreadyRegisteredException("This user already exist.");
            }
            var passwordHash = _securityService.HashPassword(request.Password);
            var user = new User() { Username = request.UserName, PasswordHash = passwordHash, Email = request.Email };
            await _userRepository.CreateAsync(user);

            return new CreateUserResponse() { Id = user.Id };
        }
    }
}
