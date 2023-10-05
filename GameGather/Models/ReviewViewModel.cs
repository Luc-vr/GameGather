using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a score")]
        [Display(Name = "Score:")]
        [Range(1, 5, ErrorMessage = "Please enter a score between 1 and 5")]
        public int Score { get; set; } // 1-5

        [Display(Name = "Text:")]
        [StringLength(500, ErrorMessage = "Please enter a text with a maximum of 500 characters")]
        public string? Text { get; set; }

        public User? User { get; set; }

        public int UserId { get; set; }

        public BoardGameNight? BoardGameNight { get; set; }

        public int BoardGameNightId { get; set; }
    }
}
