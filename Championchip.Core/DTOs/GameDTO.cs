using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.DTOs
{
    public record GameDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime Time { get; set; }
    }
}
