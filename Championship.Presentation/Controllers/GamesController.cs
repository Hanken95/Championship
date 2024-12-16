using Microsoft.AspNetCore.Mvc;
using Championchip.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Championchip.Core.Repositories;
using Championchip.Core.DTOs.GameDTOs;
using Service.Contracts;
using Championchip.Core.Responses;

namespace Championship.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamesController(IServiceManager manager) : ApiControllerBase
    {

        // GET: api/Games
        [HttpGet("games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            ApiBaseResponse response = await manager.GameService.GetAllAsync();

            return response.Success ?
                Ok(response.GetOkResult<IEnumerable<GameDTO>>()) :
                ProcessError(response);
        }

        [HttpGet("tournaments/{tournamentId:int}/games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(int tournamentId)
        {
            if (!await manager.TournamentService.AnyAsync()) return NotFound("No tournaments in the database");

            ApiBaseResponse response = await manager.GameService.GetAllAsync(tournamentId);

            return response.Success ?
                Ok(response.GetOkResult<IEnumerable<GameDTO>>()) :
                ProcessError(response);
        }

        [HttpGet("games/{title}")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(string title)
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
            ApiBaseResponse response = await manager.GameService.GetAsync(id);

            return response.Success ?
                Ok(response.GetOkResult<GameDTO>()) :
                ProcessError(response);
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("games")]
        public async Task<ActionResult<GameDTO>> PostGame(GameCreateDTO game)
        {
            ApiBaseResponse response = await manager.GameService.AddAsync(game);

            return response.Success ?
                Ok(response.GetOkResult<GameDTO>()) :
                ProcessError(response);
        }

        // DELETE: api/Games/5
        [HttpDelete("games/{id:int}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            ApiBaseResponse response = await manager.GameService.RemoveAsync(id);

            return response.Success ?
                Ok(response.GetOkResult<GameDTO>()) :
                ProcessError(response);
        }


        [HttpPatch("games/{id:int}")]
        public async Task<ActionResult<GameDTO>> PatchGame(int id, JsonPatchDocument<GameUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }
            if (!await manager.GameService.AnyAsync(g => g.Id == id))
            {
                return NotFound($"There is no game with id: {id}");
            }

            ApiBaseResponse response = await manager.GameService.PatchAsync(id, patchDocument);

            return response.Success ?
                Ok(response.GetOkResult<GameDTO>()) :
                ProcessError(response);
        }
    }
}
