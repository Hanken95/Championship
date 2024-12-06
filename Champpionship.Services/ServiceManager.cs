using AutoMapper;
using Championchip.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Services
{
    public class ServiceManager
    {
        private readonly Lazy<ITournamentService> tournamentServic;
        private readonly Lazy<IGameService> gameService;

        public ITournamentServicee TournamentService => tournamentServic.Value;
        public IGameService GameService => gameService.Value;

        public ServiceManager(IUnitOfWork unit, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(nameof(unit));

            tournamentService = new Lazy<ITournamentService>(() => new TournamentService(unit, mapper));
            gameService = new Lazy<IGameService>(() => new GameService(unit, mapper));
        }
    }
}
