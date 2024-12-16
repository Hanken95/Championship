using Championchip.Core.Repositories;
using Championship.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class UnitOfWork(ChampionshipContext context, Lazy<ITournamentRepository> tournamentRepository, Lazy<IGameRepository> gameRepository) : IUnitOfWork
    {
        public ITournamentRepository TournamentRepository { get; } = tournamentRepository.Value;
        public IGameRepository GameRepository { get; } = gameRepository.Value;

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }

    
}
