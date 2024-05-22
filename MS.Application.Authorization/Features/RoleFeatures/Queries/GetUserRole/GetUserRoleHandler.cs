using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Application.Authorization.Common.Interfaces;
using System.Linq;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole
{
    public sealed class GetUserRoleHandler(IAuthorizationDbContext authorizationDbContext, IMapper mapper) : IRequestHandler<GetUserRoleRequest, List<GetUserRoleResponse>>
    {
        private readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetUserRoleResponse>> Handle(GetUserRoleRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _authorizationDbContext.UserRoles
            .Where(s => s.UserId == request.userId)
            .Include(ur => ur.Role)
            .ToListAsync(cancellationToken);

            return _mapper.Map<List<GetUserRoleResponse>>(userRoles);
        }
    }
}
