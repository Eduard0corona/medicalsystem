using AutoMapper;
using MediatR;
using MS.Application.Authorization.Common.Exceptions;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepository, ISecurityService securityService, IMapper mapper) : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        readonly IUserRepository _userRepository = userRepository;
        readonly ISecurityService _securityService = securityService;
        readonly IMapper _mapper = mapper;

        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetBy(s => s.Username == request.UserName, cancellationToken);

            if(userExist.Count > 0) 
            {
                return Result<CreateUserResponse>.Failure("This user already exist.");
            }
            var passwordHash = _securityService.HashPassword(request.Password);

            var user = _mapper.Map<User>(request);

            await _userRepository.CreateAsync(user);

            return Result<CreateUserResponse>.Success(_mapper.Map<CreateUserResponse>(user));
        }
    }
}
