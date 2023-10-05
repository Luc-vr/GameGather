using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BoardGameViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the board game")]
        [Display(Name = "Name:")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a description of the board game")]
        [Display(Name = "Description:")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

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

        public string ImageBase64
        {
            get
            {
                if (Image != null)
                {
                    return Convert.ToBase64String(Image);
                } else
                {
                    return "";
                }
            }
        }

        public ICollection<BoardGameNight>? BoardGameNights { get; set; }
    }
}
