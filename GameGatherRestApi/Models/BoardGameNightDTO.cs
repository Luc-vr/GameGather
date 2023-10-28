namespace GameGatherRestApi.Models
{
    public class BoardGameNightDTO
    {
        public int Id { get; set; }
        public int MaxAttendees { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsAdultOnly { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public FoodAndDrinksPreferenceDTO? FoodAndDrinksPreference { get; set; }
        public int HostId { get; set; }
        public HostDTO? Host { get; set; }
        public ICollection<String>? BoardGames { get; set; }
    }
}
