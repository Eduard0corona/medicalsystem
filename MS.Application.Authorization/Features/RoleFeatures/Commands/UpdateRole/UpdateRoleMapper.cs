using AutoMapper;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.UpdateRole
{
    public class UpdateRoleMapper : Profile
    {
        public UpdateRoleMapper() 
        {
            CreateMap<Role, UpdateRoleResponse>();
        }
    }
}
