using AutoMapper;
using Championchip.Core.DTOs.GameDTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championchip.Core.Request;
using Championchip.Core.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
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

        public async Task<ApiBaseResponse> GetAllAsync()
        {
            return new ApiOkResponse<IEnumerable<GameDTO>>(mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync()));
        }

        public async Task<ApiBaseResponse> GetAllAsync(int tournamentId)
        {
            if (!await unit.TournamentRepository.AnyAsync(t => t.Id == tournamentId)) return new TournamentNotFoundResponse(tournamentId);
            return new ApiOkResponse<IEnumerable<GameDTO>>(mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync(tournamentId)));
        }

        public async Task<ApiBaseResponse> GetAllAsync(Expression<Func<Game, bool>> conditions)
        {
            return new ApiOkResponse<IEnumerable<GameDTO>>(mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync(conditions)));
        }

        public async Task<ApiBaseResponse> GetAsync(Expression<Func<Game, bool>> conditions)
        {
            Game? game = await unit.GameRepository.GetAsync(conditions);
            if (game == null) return new GameNotFoundResponse(conditions);
            GameDTO gameDTO = mapper.Map<GameDTO>(game);
            return new ApiOkResponse<GameDTO>(gameDTO);
        }
        public async Task<ApiBaseResponse> GetAsync(int id)
        {
            Game? game = await unit.GameRepository.GetAsync(id);
            if (game == null) return new GameNotFoundResponse(id);
            GameDTO gameDTO = mapper.Map<GameDTO>(game);
            return new ApiOkResponse<GameDTO>(gameDTO);
        }

        public async Task<ApiBaseResponse> AddAsync(GameCreateDTO dto)
        {
            TournamentRequestParams requestParams = new TournamentRequestParams() { IncludeGames = true };
            Tournament? tournament = await unit.TournamentRepository.GetAsync(dto.TournamentId, requestParams);
            if (tournament == null) return new TournamentNotFoundResponse(dto.TournamentId);
            if (tournament.Games.Count > 9) return new TournamentIsFullResponse(dto.TournamentId);
            Game game = mapper.Map<Game>(dto);
            unit.GameRepository.Add(game);

            await unit.CompleteAsync();

            return new ApiOkResponse<GameDTO>(mapper.Map<GameDTO>(game));
        }

        public async Task<ApiBaseResponse> RemoveAsync(int id)
        {
            Game? game = await unit.GameRepository.GetAsync(t => t.Id == id);
            if (game == null) return new GameNotFoundResponse(id);
            unit.GameRepository.Remove(game);
            return new ApiRemovedResponse<Game>(id);
        }

        public async Task<ApiBaseResponse> PutAsync(int id, GameUpdateDTO dto)
        {
            Game? gametoPut = await unit.GameRepository.GetAsync(t => t.Id == id);
            if (gametoPut == null) return new GameNotFoundResponse(id);

            mapper.Map(dto, gametoPut);

            await unit.CompleteAsync();

            return new ApiOkResponse<GameDTO>(mapper.Map<GameDTO>(gametoPut));
        }

        public async Task<ApiBaseResponse> PatchAsync(int id, JsonPatchDocument<GameUpdateDTO> patchDocument)
        {
            var gameToPatch = await unit.GameRepository.GetAsync(g => g.Id == id);

            if (gameToPatch == null) return new GameNotFoundResponse(id);

            var dto = mapper.Map<GameUpdateDTO>(gameToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, gameToPatch);
            await unit.CompleteAsync();

            return new ApiOkResponse<GameDTO>(mapper.Map<GameDTO>(gameToPatch));
        }
    }
}