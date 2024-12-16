using Championchip.Core.Entities;

namespace Championchip.Core.Repositories
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<IEnumerable<Game>> GetAllAsync(int tournamentId);
    }
}