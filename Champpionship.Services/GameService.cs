using AutoMapper;
using Championchip.Core.DTOs.GameDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Championship.Services
{
    public class GameService(IUnitOfWork unit, IMapper mapper) : IGameService
    {
        public async Task<bool> AnyAsync()
        {
            return await unit.GameRepository.AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Game, bool>> conditions)
        {
            return await unit.GameRepository.AnyAsync(conditions);
        }

        public async Task<IEnumerable<GameDTO>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync());
        }

        public async Task<IEnumerable<GameDTO>> GetAllAsync(Expression<Func<Game, bool>> conditions)
        {
            return mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync(conditions));
        }

        public async Task<GameDTO?> GetAsync(Expression<Func<Game, bool>> conditions)
        {
            return mapper.Map<GameDTO>(await unit.GameRepository.GetAsync(conditions));
        }

        public async Task<GameDTO> AddAsync(GameCreateDTO dto)
        {
            Game tournament = mapper.Map<Game>(dto);
            unit.GameRepository.Add(tournament);

            await unit.CompleteAsync();

            return mapper.Map<GameDTO>(tournament);
        }

        public async Task RemoveAsync(int id)
        {
            Game? entity = await unit.GameRepository.GetAsync(t => t.Id == id) ?? throw new Exception("Game not found");
            unit.GameRepository.Remove(entity);
        }

        public async Task PutAsync(int id, GameUpdateDTO dto)
        {
            mapper.Map(dto, await unit.GameRepository.GetAsync(t => t.Id == id));

            await unit.CompleteAsync();
        }

        public async Task PatchAsync(int id, JsonPatchDocument<GameUpdateDTO> patchDocument)
        {
            var tournamentToPatch = await unit.GameRepository.GetAsync(g => g.Id == id);

            var dto = mapper.Map<GameUpdateDTO>(tournamentToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, tournamentToPatch);
            await unit.CompleteAsync();
        }
    }
}