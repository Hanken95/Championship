using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.Entities
{
    public class Tournament
    {
        public int Id {  get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Game> Games { get; set; } = [];

    }
}
