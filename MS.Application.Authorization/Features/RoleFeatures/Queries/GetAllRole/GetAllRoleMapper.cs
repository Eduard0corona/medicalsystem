using AutoMapper;
using MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetAllRole
{
    public class GetAllRoleMapper : Profile
    {
        public GetAllRoleMapper()
        {
            CreateMap<Role, GetAllRoleResponse>();
        }
    }
}
