using Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name:")]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name:")]
        [MaxLength(255)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [Display(Name = "Email address:")]
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please pick your gender")]
        [Display(Name = "Gender:")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please enter your birth date")]
        [Display(Name = "Birth date:")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

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

        [InverseProperty(nameof(BoardGameNight.Attendees))]
        public ICollection<BoardGameNight>? AttendingBoardGameNights { get; set; }

        [InverseProperty(nameof(BoardGameNight.Host))]
        public ICollection<BoardGameNight>? HostingBoardGameNights { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
