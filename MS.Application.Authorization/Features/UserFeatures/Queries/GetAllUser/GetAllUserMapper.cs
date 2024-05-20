using AutoMapper;
using MS.Domain.Authorization.Entities;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.GetAllUser
{
    public class GetAllUserMapper : Profile
    {
        public GetAllUserMapper()
        {
            CreateMap<User, GetAllUserResponse>();
        }
    }
}
