using AutoMapper;
using MS.Domain.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole
{
    public class GetUserRoleMapper : Profile
    {
        public GetUserRoleMapper()
        {
            CreateMap<UserRole, GetUserRoleResponse>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<string> { src.Role!.Name }))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<List<UserRole>, GetUserRoleResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Select(ur => ur.Role!.Name).ToList()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.FirstOrDefault()!.UserId));
        }
    }
}
