using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Repositories;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public sealed class GetAllRoleHandler(IAuthorizationDbContext authorizationDbContext, IMapper mapper) : IRequestHandler<GetAllRoleRequest, List<GetAllRoleResponse>>
    {
        private readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetAllRoleResponse>> Handle(GetAllRoleRequest request, CancellationToken cancellationToken)
        {
            var users = await _authorizationDbContext.Roles.ToListAsync(cancellationToken);
            return _mapper.Map<List<GetAllRoleResponse>>(users);
        }
    }
}
