﻿using AutoMapper;
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

        public ServiceManager(Lazy<ITournamentService> tournamentService, Lazy<IGameService> gameService)
        {
            ArgumentNullException.ThrowIfNull(nameof(tournamentService));
            ArgumentNullException.ThrowIfNull(nameof(gameService));

            this.tournamentService = tournamentService;
            this.gameService = gameService;
        }
    }
}
