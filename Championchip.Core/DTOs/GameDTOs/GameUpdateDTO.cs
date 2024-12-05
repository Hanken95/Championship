using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.DTOs.GameDTOs
{
    public record GameUpdateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title required")]
        [Length(2, 30, ErrorMessage = "Title can only be from 2 to 30 characters")]
        public required string Title { get; init; }

        [Required(ErrorMessage = "Time required")]
        public DateTime Time { get; init; }
    }
}
