using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Championchip.Core.Entities;
using Championship.Data.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Championship.Data.Repositories;
using Championchip.Core.Repositories;
using Championchip.Core.DTOs.GameDTOs;

namespace Championship.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamesController(IMapper mapper, IUnitOfWork unit) : ControllerBase
    {

        // GET: api/Games
        [HttpGet("games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            if (!await unit.GameRepository.AnyAsync()) return NotFound("No games in the database");

            return Ok(mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync()));
        }
        [HttpGet("games/{title}")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(string title)
        {
            if (!await unit.GameRepository.AnyAsync()) return NotFound("No games in the database");

            if (string.IsNullOrWhiteSpace(title)) return BadRequest("Incorrect input");

            if (!await unit.GameRepository.AnyAsync(g => g.Title == title)) return NotFound($"No game with title '{title}' was found");

            return Ok(mapper.Map<IEnumerable<GameDTO>>(await unit.GameRepository.GetAllAsync(g => g.Title == title)));
        }

        [HttpGet("tournaments/{tournamentId:int}/games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(int tournamentId)
        {
            if (!await unit.TournamentRepository.AnyAsync()) return NotFound("No tournaments in the database");

            var tournament = await unit.TournamentRepository.GetAsync(t => t.Id == tournamentId, true);
            if (tournament == null)  return NotFound($"No tournament with id {tournamentId} in the database");

            var games = await unit.GameRepository.GetAllAsync(g => g.TournamentId == tournamentId);
            if (games.Count() == 0) return NotFound("No games in the tournament");

            return Ok(mapper.Map<IEnumerable<GameDTO>>(games));
        }

        // GET: api/Games/5
        [HttpGet("games/{id:int}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            if (!await unit.GameRepository.AnyAsync()) return NotFound("No games in the database");
            var game = await unit.GameRepository.GetAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound($"No game with id {id} exists");
            }

            return Ok(mapper.Map<GameDTO>(game));
        }

        //// PUT: api/Games/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Games
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Game>> PostGame(Game game)
        //{
        //    context.Games.Add(game);
        //    await context.SaveChangesAsync();

        //    return CreatedAtAction("GetGame", new { id = game.Id }, game);
        //}

        // DELETE: api/Games/5
        [HttpDelete("games/{id:int}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await unit.GameRepository.GetAsync(g => g.Id == id);
            if (game == null)
            {
                return NotFound($"No game with id {id} exists");
            }

            unit.GameRepository.Remove(game);
            await unit.CompleteAsync();

            return NoContent();
        }


        [HttpPatch("games/{id:int}")]
        [HttpPatch("tournaments/{tournamentId}/games/{id:int}")]
        public async Task<ActionResult> PatchGame(int? tournamentId, int id, JsonPatchDocument<GameUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }
            Game gameToPatch = null;
            if (tournamentId != null)
            {
                if (!await unit.TournamentRepository.AnyAsync(t => t.Id == tournamentId))
                {
                    return NotFound("Chosen tournament not found in database");
                }
                gameToPatch = await unit.GameRepository.GetAsync(g => g.TournamentId == tournamentId && g.Id == id);
                if (gameToPatch == null)
                {
                    return NotFound($"The tournament does not contain a game with id: {id}");
                }
            }
            else
            {
                gameToPatch = await unit.GameRepository.GetAsync(g => g.Id == id);
                if (gameToPatch == null)
                {
                    return NotFound($"There is no game with id: {id}");
                }
            }



            var dto = mapper.Map<GameUpdateDTO>(gameToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, gameToPatch);
            await unit.CompleteAsync();

            return NoContent();
        }
    }
}
