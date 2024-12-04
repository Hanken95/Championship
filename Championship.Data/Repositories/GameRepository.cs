using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championship.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class GameRepository(ChampionshipContext context) : RepositoryBase<Game>(context) ,IGameRepository
    {

    }
}
