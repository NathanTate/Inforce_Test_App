using AutoMapper;
using Inforce_Api.Models.DTO.User.Requests;
using Inforce_Api.Models.DTO.User.Responses;
using Inforce_Api.Models;
using Inforce_Api.Models.DTO.ShortenUrl.Responses;

namespace Inforce_Api.Helpers
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
                config.CreateMap<ApplicationUser, UserResponse>();

                config.CreateMap<ShortenUrl, ShortenUrlResponse>();
            });
        }
    }
}
