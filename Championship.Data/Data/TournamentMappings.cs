using AutoMapper;
using Championchip.Core.DTOs.GameDTOs;
using Championchip.Core.DTOs.TournamentDTOs;
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
            CreateMap<Tournament, TournamentUpdateDTO>();
            CreateMap<TournamentCreateDTO, Tournament>();
            CreateMap<TournamentUpdateDTO, Tournament>();

            CreateMap<Game, GameDTO>();
            CreateMap<Game, GameUpdateDTO>();
            CreateMap<GameUpdateDTO, Game>().ReverseMap();
            CreateMap<GameCreateDTO, Game>().ReverseMap();
        }
    }
}
