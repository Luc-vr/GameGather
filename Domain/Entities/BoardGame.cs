using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BoardGame
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the board game")]
        [Display(Name = "Name:")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a description of the board game")]
        [Display(Name = "Description:")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set;}

        [Required(ErrorMessage = "Please select a genre")]
        [Display(Name = "Genre:")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please select a game type")]
        [Display(Name = "Game type:")]
        public GameType GameType { get; set; }

        [Required(ErrorMessage = "Please specify if the game is 18+")]
        [Display(Name = "Adult only:")]
        public bool IsAdultOnly { get; set; }

        [Display(Name = "Image:")]
        public byte[]? Image { get; set; }

        public ICollection<BoardGameNight>? BoardGameNights { get; set; }

    }
}
