using Championchip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.Repositories
{
    public interface ITournamentRepository : IRepositoryBase<Tournament>
    {
        Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames);
        Task<Tournament?> GetAsync(Expression<Func<Tournament, bool>> conditions, bool includeGames);
    }
}
