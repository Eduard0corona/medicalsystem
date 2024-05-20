﻿using AutoMapper;
using MediatR;
using MS.Application.Authorization.Common.Security;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser
{
    public sealed class GetAllUserHandler : IRequestHandler<GetAllUserRequest, List<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllUserResponse>>(users);
        }
    }
}