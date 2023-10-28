using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a score")]
        [Display(Name = "Score:")]
        [Range(1, 5, ErrorMessage = "Please enter a score between 1 and 5")]
        public int Score { get; set; } // 1-5

        [Display(Name = "Text:")]
        [DataType(DataType.MultilineText)]
        public string? Text { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("BoardGameNightId")]
        public BoardGameNight? BoardGameNight { get; set; }

        public int BoardGameNightId { get; set; }
    }
}
