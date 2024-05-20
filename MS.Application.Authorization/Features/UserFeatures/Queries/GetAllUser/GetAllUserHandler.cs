using AutoMapper;
using MediatR;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser
{
    public sealed class GetAllUserHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllUserRequest, List<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllUserResponse>>(users);
        }
    }
}
