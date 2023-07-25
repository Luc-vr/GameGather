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

        [Required]
        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Gender:")]
        public Gender? Gender { get; set; }

        [Required]
        [Display(Name = "Birth date:")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "City:")]
        public string? City { get; set; }

        [Required]
        public string? Address { get; set; }

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
