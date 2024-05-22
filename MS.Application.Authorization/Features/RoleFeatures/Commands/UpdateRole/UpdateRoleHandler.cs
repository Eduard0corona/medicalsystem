using AutoMapper;
using MediatR;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.UpdateRole
{
    public class UpdateRoleHandler(IAuthorizationDbContext authorizationDbContext, IMapper mapper) : IRequestHandler<UpdateRoleRequest, Result<UpdateRoleResponse>>
    {
        readonly IAuthorizationDbContext _authorizationDbContext = authorizationDbContext;
        readonly IMapper _mapper = mapper;

        public async Task<Result<UpdateRoleResponse>> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var entity = await _authorizationDbContext.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

            if(entity == null)
            {
                return Result<UpdateRoleResponse>.Failure("Role not found.");
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _authorizationDbContext.SaveChangesAsync(cancellationToken);

            return Result<UpdateRoleResponse>.Success(_mapper.Map<UpdateRoleResponse>(entity));
        }
    }
}
