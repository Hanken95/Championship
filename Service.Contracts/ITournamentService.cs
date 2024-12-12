using Championchip.Core.DTOs.TournamentDTOs;
using Championchip.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Service.Contracts
{
    public interface ITournamentService
    {
        Task<TournamentDTO> AddAsync(TournamentCreateDTO dto);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<Tournament, bool>> conditions);
        Task<IEnumerable<TournamentDTO>> GetAllAsync(bool includeGames);
        Task<IEnumerable<TournamentDTO>> GetAllAsync(Expression<Func<Tournament, bool>> conditions);
        Task<TournamentDTO?> GetAsync(Expression<Func<Tournament, bool>> conditions, bool includeGames = false);
        Task PatchAsync(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument);
        Task PutAsync(int id, TournamentUpdateDTO dto);
        Task RemoveAsync(int id);
    }
}