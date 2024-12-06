using AutoMapper;
using Championchip.Core.Repositories;

namespace Championship.Services
{
    internal class GameService
    {
        private IUnitOfWork _unit;
        private IMapper _mapper;

        public GameService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
    }
}