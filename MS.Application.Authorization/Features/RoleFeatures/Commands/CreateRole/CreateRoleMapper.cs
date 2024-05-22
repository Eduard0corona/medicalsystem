using AutoMapper;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.CreateRole
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper()
        {
            CreateMap<Role, CreateRoleResponse>();
        }
    }
}
