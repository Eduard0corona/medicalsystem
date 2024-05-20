using AutoMapper;
using MediatR;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public sealed class GetAllRoleHandler : IRequestHandler<GetAllRoleRequest, List<GetAllRoleResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllRoleHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllRoleResponse>> Handle(GetAllRoleRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllRoleResponse>>(users);
        }
    }
}
