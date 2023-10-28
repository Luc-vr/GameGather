using System.ComponentModel.DataAnnotations;

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
