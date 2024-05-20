using AutoMapper;
using MediatR;
using MS.Application.Authorization.Common.Exceptions;
using MS.Application.Authorization.Common.Interfaces;
using MS.Application.Authorization.Common.Models;
using MS.Application.Authorization.Common.Security;
using MS.Application.Authorization.Repositories;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.CreateRole
{
    [Authorize(Roles = "Admin")]
    public class CreateRoleHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<CreateRoleRequest, Result<CreateRoleResponse>>
    {
        readonly IRoleRepository _roleRepository  = roleRepository;
        readonly IMapper _mapper = mapper;

        public async Task<Result<CreateRoleResponse>> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.GetBy(s => s.Name.Contains(request.Name), cancellationToken);

            if (roleExist.Count > 0)
            {
                return Result<CreateRoleResponse>.Failure("This role already exist.");
            }

            var role = new Role() { Name = request.Name, Description = request.Description };
            await _roleRepository.CreateAsync(role);

            return Result<CreateRoleResponse>.Success(_mapper.Map<CreateRoleResponse>(role));
        }
    }
}
