using AutoMapper;
using MS.Application.Authorization.Features.RoleFeatures.Queries.GetUserRole;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserMapper : Profile
    {
        public LoginUserMapper()
        {
            CreateMap<Token, LoginUserResponse>()
           .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Value));
        }
    }
}
