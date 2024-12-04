using AutoMapper;
using Championchip.Core.DTOs;
using Championchip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Data
{
    public class TournamentMappings :Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDTO>();
            CreateMap<TournamentCreateDTO, Tournament>();
            CreateMap<TournamentUpdateDTO, Tournament>();

            CreateMap<Game, GameDTO>();
            CreateMap<GameUpdateDTO, Game>().ReverseMap();
        }
    }
}
