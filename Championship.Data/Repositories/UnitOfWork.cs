using Championchip.Core.Repositories;
using Championship.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class UnitOfWork(ChampionshipContext context) : IUnitOfWork
    {

        public ITournamentRepository TournamentRepository { get; } = new TournamentRepository(context);

        public IGameRepository GameRepository { get; } = new GameRepository(context);

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }

    
}
