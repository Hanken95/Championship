using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.DTOs
{
    public class TournamentCreateDTO
    {
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
