using Championchip.Core.DTOs.TournamentDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Service.Contracts
{
    public interface ITournamentService
    {
        Task<TournamentDTO> AddAsync(TournamentCreateDTO dto);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<Tournament, bool>> conditions);
        Task<(IEnumerable<TournamentDTO> companyDtos, MetaData metaData)> GetAllAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  requestParams);
        Task<(IEnumerable<TournamentDTO> companyDtos, MetaData metaData)> GetAllAsync(TournamentRequestParams requestParams);
        Task<TournamentDTO?> GetAsync(int id);
        Task<TournamentDTO?> GetAsync(int id, TournamentRequestParams requestParams);
        Task<TournamentDTO?> GetAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  requestParams);
        Task<TournamentDTO> PatchAsync(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument);
        Task<TournamentDTO> PutAsync(int id, TournamentUpdateDTO dto);
        Task RemoveAsync(int id);
    }
}