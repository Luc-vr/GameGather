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

        [Required]
        public int MaxAttendees { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool IsAdultOnly { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Address { get; set; }

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
