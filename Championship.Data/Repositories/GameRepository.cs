using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championship.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class GameRepository(ChampionshipContext context) : RepositoryBase<Game>(context), IGameRepository
    {
        public async Task<IEnumerable<Game>> GetAllAsync(int tournamentId)
        {
            return await GetAllAsync(g => g.TournamentId == tournamentId);
        }
    }
}
