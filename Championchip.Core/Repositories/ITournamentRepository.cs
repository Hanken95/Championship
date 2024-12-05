using Championchip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.Repositories
{
    public interface ITournamentRepository : IRepositoryBase<Tournament>
    {
        public  Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames);
    }
}
