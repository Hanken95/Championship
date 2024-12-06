using AutoMapper;
using Championchip.Core.Repositories;

namespace Championship.Services
{
    public class TournamentService
    {
        private IUnitOfWork _unit;
        private IMapper _mapper;

        public TournamentService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
    }
}