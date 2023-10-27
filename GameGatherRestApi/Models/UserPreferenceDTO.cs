namespace GameGatherRestApi.Models
{
    public class UserPreferenceDTO
    {
        public bool LactoseFree { get; set; } = false;
        public bool NutFree { get; set; } = false;
        public bool AlcoholFree { get; set; } = false;
        public bool Vegetarian { get; set; } = false;
    }
}
