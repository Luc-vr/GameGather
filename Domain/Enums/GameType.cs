using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Domain.Enums
{
    public enum GameType
    {
        [Display(Name = "Card games")]
        Cards,
        [Display(Name = "Board games")]
        Board,
        [Display(Name = "Video games")]
        VideoGame,
        [Display(Name = "Dice games")]
        Dice,
        [Display(Name = "Puzzle games")]
        Puzzle,
    }
}
