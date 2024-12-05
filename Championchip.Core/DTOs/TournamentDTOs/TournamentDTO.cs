using Championchip.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.DTOs.TournamentDTOs
{
    public record TournamentDTO
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title required")]
        [Length(2, 30, ErrorMessage = "Title can only be from 2 to 30 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Start time required")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(3);
        public ICollection<Game> Games { get; set; } = [];
    }
}
