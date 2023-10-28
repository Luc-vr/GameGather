using Domain.Enums;
using HotChocolate;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BoardGame
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public GameType GameType { get; set; }

        [Required]
        public bool IsAdultOnly { get; set; }

        [GraphQLIgnore]
        public byte[]? Image { get; set; }

        public ICollection<BoardGameNight>? BoardGameNights { get; set; }

    }
}
