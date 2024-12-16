using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.DTOs.GameDTOs
{
    public record GameCreateDTO : GameForManupulationDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tournament Id required")]
        [DisplayName("Tournament Id")]
        public int TournamentId { get; set; }
    }
}
