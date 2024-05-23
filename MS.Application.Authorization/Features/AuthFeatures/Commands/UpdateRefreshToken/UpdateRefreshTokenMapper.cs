using AutoMapper;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenMapper : Profile
    {
        public UpdateRefreshTokenMapper()
        {
            CreateMap<RefreshToken, UpdateRefreshTokenResponse>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token));
        }
    }
}
