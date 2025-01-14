using AutoMapper;
using Domain.Entities;
using GameGatherRestApi.Models;

namespace GameGatherRestApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserPreferenceDTO>().ReverseMap();
            CreateMap<User, UserProfileDTO>().ReverseMap();

            CreateMap<User, HostDTO>().ReverseMap();
            CreateMap<FoodAndDrinksPreference, FoodAndDrinksPreferenceDTO>().ReverseMap();
            CreateMap<BoardGameNight, BoardGameNightDTO>().ForMember(
                dest => dest.BoardGames,
                opt => opt.MapFrom(src => src.BoardGames!.Select(bg => bg.Name).ToList()))
                .ReverseMap();
        }
    }
}
