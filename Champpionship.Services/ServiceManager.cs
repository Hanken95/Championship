using AutoMapper;
using Championchip.Core.Repositories;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;

        public ITournamentService TournamentService => tournamentService.Value;
        public IGameService GameService => gameService.Value;

        public ServiceManager(IUnitOfWork unit, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(nameof(unit));
            ArgumentNullException.ThrowIfNull(nameof(mapper));

            tournamentService = new Lazy<ITournamentService>(() => new TournamentService(unit, mapper));
            gameService = new Lazy<IGameService>(() => new GameService(unit, mapper));
        }
    }
}
