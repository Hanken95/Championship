using Championchip.Core.DTOs.GameDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<ApiBaseResponse> AddAsync(GameCreateDTO dto);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<Game, bool>> conditions);
        Task<ApiBaseResponse> GetAllAsync();
        Task<ApiBaseResponse> GetAllAsync(int tournamentId);
        Task<ApiBaseResponse> GetAllAsync(Expression<Func<Game, bool>> conditions);
        Task<ApiBaseResponse> GetAsync(Expression<Func<Game, bool>> conditions);
        Task<ApiBaseResponse> GetAsync(int id);
        Task<ApiBaseResponse> PatchAsync(int id, JsonPatchDocument<GameUpdateDTO> patchDocument);
        Task<ApiBaseResponse> PutAsync(int id, GameUpdateDTO dto);
        Task<ApiBaseResponse> RemoveAsync(int id);
    }
}