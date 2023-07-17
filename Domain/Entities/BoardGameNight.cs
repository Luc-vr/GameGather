using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BoardGameNight
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the maximum number of attendees")]
        [Display(Name = "Maximum Attendees:")]
        public int MaxAttendees { get; set; }

        [Required(ErrorMessage = "Please enter the date and time of the board game night")]
        [Display(Name = "Date and Time:")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please specify if the game night is for adults only")]
        [Display(Name = "Adult Only:")]
        public bool IsAdultOnly { get; set; }

        [Required(ErrorMessage = "Please enter the city")]
        [Display(Name = "City:")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter the street")]
        [Display(Name = "Street:")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Please enter the house number")]
        [Display(Name = "House Number:")]
        public int HouseNumber { get; set; }

        [ForeignKey("FoodAndDrinksPreferenceId")]
        public FoodAndDrinksPreference? FoodAndDrinksPreference { get; set; }

        public int FoodAndDrinksPreferenceId { get; set; }
        [ForeignKey(nameof(Host))]
        public int HostId { get; set; }

        [InverseProperty(nameof(User.HostingBoardGameNights))]
        public User? Host { get; set; }

        [InverseProperty(nameof(User.AttendingBoardGameNights))]
        public ICollection<User>? Attendees { get; set; }

        public ICollection<BoardGame>? BoardGames { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
