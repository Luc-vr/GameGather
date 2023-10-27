using AutoMapper;
using Web.Models;
using Domain.Entities;

namespace Test.MappingProfiles
{
    public class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            // User mappings
            CreateMap<User, ProfileViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();

            // BoardGameNight mappings
            CreateMap<BoardGameNight, BoardGameNightViewModel>().ReverseMap();

            // FoodAndDrinksPreference mappings
            CreateMap<UserPreferencesViewModel, FoodAndDrinksPreference>().ReverseMap();
            CreateMap<EventPreferencesViewModel, FoodAndDrinksPreference>().ReverseMap();

            // Review mappings
            CreateMap<Review, ReviewViewModel>().ReverseMap();

            // BoardGame mappings (do not map the image when the source is null)
            CreateMap<BoardGame, BoardGameViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image != null))
                .ReverseMap();
        }
    }
}
