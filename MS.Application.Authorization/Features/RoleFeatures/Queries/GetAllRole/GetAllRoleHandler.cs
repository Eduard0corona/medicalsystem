using AutoMapper;
using MediatR;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public sealed class GetAllRoleHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllRoleRequest, List<GetAllRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetAllRoleResponse>> Handle(GetAllRoleRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllRoleResponse>>(users);
        }
    }
}
