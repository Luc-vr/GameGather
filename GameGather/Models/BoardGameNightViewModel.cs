using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class BoardGameNightViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the maximum number of attendees")]
        [Display(Name = "Maximum amount of attendees:")]
        public int MaxAttendees { get; set; } = 3;

        [Required(ErrorMessage = "Please enter the date and time of the board game night")]
        [Display(Name = "Date and Time:")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [NotInPast(ErrorMessage = "The date and time cannot be in the past")]
        public DateTime DateTime { get; set; } = DateTime.Now.AddDays(7).Date.AddHours(20);


        [Required(ErrorMessage = "Please specify if the game night is for adults only")]
        [Display(Name = "Only for adults (18+):")]
        public bool IsAdultOnly { get; set; } = false;

        [Required(ErrorMessage = "Please enter the name of your city")]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        [RegularExpression(@"^[A-Za-z\s]+ \d+[A-Za-z]{0,2}$", ErrorMessage = "Invalid address format. Example: Street 123 or Street 1A")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        public User? Host { get; set; }

        public EventPreferencesViewModel? FoodAndDrinksPreference { get; set; }

        public ICollection<BoardGame>? BoardGames { get; set; }

        public ICollection<User>? Attendees { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public bool IsHost { get; set; }

        public bool IsAttending { get; set; }

        public bool HasAdultGames()
        {
            return BoardGames!.Any(bg => bg.IsAdultOnly);
        }

    }
}
