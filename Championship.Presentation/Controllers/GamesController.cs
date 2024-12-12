using Microsoft.AspNetCore.Mvc;
using Championchip.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Championchip.Core.Repositories;
using Championchip.Core.DTOs.GameDTOs;
using Service.Contracts;

namespace Championship.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamesController(IServiceManager manager) : ControllerBase
    {

        // GET: api/Games
        [HttpGet("games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            if (!await manager.GameService.AnyAsync()) return NotFound("No games in the database");

            return Ok(await manager.GameService.GetAllAsync());
        }

        [HttpGet("tournaments/{tournamentId:int}/games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(int tournamentId)
        {
            if (!await manager.TournamentService.AnyAsync()) return NotFound("No tournaments in the database");

            var tournament = await manager.TournamentService.GetAsync(t => t.Id == tournamentId, true);
            if (tournament == null) return NotFound($"No tournament with id {tournamentId} in the database");

            var games = await manager.GameService.GetAllAsync(g => g.TournamentId == tournamentId);
            if (!games.Any()) return NotFound("No games in the tournament");

            return Ok(games);
        }

        [HttpGet("games/{title}")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGame(string title)
        {
            if (!await manager.GameService.AnyAsync()) return NotFound("No games in the database");

            if (string.IsNullOrWhiteSpace(title)) return BadRequest("Incorrect input");

            if (!await manager.GameService.AnyAsync(g => g.Title == title)) return NotFound($"No game with title '{title}' was found");

            return Ok(await manager.GameService.GetAllAsync(g => g.Title == title));
        }


        // GET: api/Games/5
        [HttpGet("games/{id:int}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            if (!await manager.GameService.AnyAsync()) return NotFound("No games in the database");
            var game = await manager.GameService.GetAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound($"No game with id {id} exists");
            }

            return Ok(game);
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
            if (!await manager.GameService.AnyAsync())
            {
                return NotFound($"No game with id {id} exists");
            }

            await manager.GameService.RemoveAsync(id);

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
            if (await manager.GameService.AnyAsync(g => g.Id == id))
            {
                return NotFound($"There is no game with id: {id}");
            }
            if (tournamentId != null)
            {
                if (!await manager.TournamentService.AnyAsync(t => t.Id == tournamentId))
                {
                    return NotFound("Chosen tournament not found in database");
                }
                if (!await manager.GameService.AnyAsync(g => g.TournamentId == tournamentId && g.Id == id))
                {
                    return NotFound($"The tournament does not contain a game with id: {id}");
                }
            }

            await manager.GameService.PatchAsync(id, patchDocument);

            return NoContent();
        }
    }
}
