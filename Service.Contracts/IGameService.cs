using Championchip.Core.DTOs.GameDTOs;
using Championchip.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<GameDTO> AddAsync(GameCreateDTO dto);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<Game, bool>> conditions);
        Task<IEnumerable<GameDTO>> GetAllAsync();
        Task<IEnumerable<GameDTO>> GetAllAsync(Expression<Func<Game, bool>> conditions);
        Task<GameDTO?> GetAsync(Expression<Func<Game, bool>> conditions);
        Task PatchAsync(int id, JsonPatchDocument<GameUpdateDTO> patchDocument);
        Task PutAsync(int id, GameUpdateDTO dto);
        Task RemoveAsync(int id);
    }
}