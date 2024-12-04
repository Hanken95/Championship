using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Championchip.Core.DTOs
{
    public record TournamentForManupulationDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title required")]
        [Length(2, 30, ErrorMessage = "Title can only be from 2 to 30 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Start time required")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
    }
}
