using AutoMapper;
using Web.Models;
using Domain.Entities;

namespace Web.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, ProfileViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();

            // BoardGameNight mappings
            CreateMap<BoardGameNight, BoardGameNightViewModel>().ReverseMap();

            // FoodAndDrinksPreference mappings
            CreateMap<UserPreferencesViewModel, FoodAndDrinksPreference>().ReverseMap();
            CreateMap<EventPreferencesViewModel, FoodAndDrinksPreference>().ReverseMap();
        }
    }
}
