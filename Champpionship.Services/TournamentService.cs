using AutoMapper;
using Championchip.Core.DTOs.TournamentDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace Championship.Services
{
    public class TournamentService(IUnitOfWork unit, IMapper mapper) : ITournamentService
    {
        public async Task<TournamentDTO> AddAsync(TournamentCreateDTO dto)
        {
            Tournament tournament = mapper.Map<Tournament>(dto);
            unit.TournamentRepository.Add(tournament);

            await unit.CompleteAsync();

            return mapper.Map<TournamentDTO>(tournament);
        }

        public async Task<bool> AnyAsync(Expression<Func<Tournament, bool>> conditions)
        {
            return await unit.TournamentRepository.AnyAsync(conditions);
        }

        public async Task<bool> AnyAsync()
        {
            return await unit.TournamentRepository.AnyAsync();
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllAsync(bool includeGames)
        {
            return mapper.Map<IEnumerable<TournamentDTO>>(await unit.TournamentRepository.GetAllAsync(includeGames));
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllAsync(Expression<Func<Tournament, bool>> conditions)
        {
            return mapper.Map<IEnumerable<TournamentDTO>>(await unit.TournamentRepository.GetAllAsync(conditions));
        }

        public async Task<TournamentDTO?> GetAsync(Expression<Func<Tournament, bool>> conditions, bool includeGames = false)
        {
            return mapper.Map<TournamentDTO>(await unit.TournamentRepository.GetAsync(conditions, includeGames));
        }

        public async Task RemoveAsync(int id)
        {
            Tournament? entity = await unit.TournamentRepository.GetAsync(t => t.Id == id) ?? throw new NullReferenceException("Tournament not found");
            unit.TournamentRepository.Remove(entity);
        }

        public async Task PutAsync(int id, TournamentUpdateDTO dto)
        {
            mapper.Map(dto, await unit.TournamentRepository.GetAsync(t => t.Id == id));

            await unit.CompleteAsync();
        }

        public async Task PatchAsync(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument)
        {
            var tournamentToPatch = await unit.TournamentRepository.GetAsync(g => g.Id == id);

            var dto = mapper.Map<TournamentUpdateDTO>(tournamentToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, tournamentToPatch);
            await unit.CompleteAsync();
        }
    }
}