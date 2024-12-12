using System.ComponentModel.DataAnnotations;

namespace Championchip.Core.Entities
{
    public class Game
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title required")]
        [Length(2, 30, ErrorMessage = "Title can only be from 2 to 30 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Time required")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Games must be a part of a tournament")]
        public int TournamentId { get; set; }
    }
}