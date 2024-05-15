using MediatR;
using Microsoft.Extensions.Configuration;
using MS.Application.Authorization.Common.Exceptions;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;
using MS.Application.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepository, IConfiguration configuration, IJwtService jwtService) : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly IConfiguration _configuration = configuration;
        readonly IJwtService _jwtService = jwtService;

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetBy(s => s.Username == request.UserName, cancellationToken);
            if(userExist.Count > 0) 
            {
                throw new UserAlreadyRegisteredException("This user already exist.");
            }
        }
    }
}
