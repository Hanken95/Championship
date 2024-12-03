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
using Championchip.Core.DTOs;

namespace Championship.API.Controllers
{
    [Route("api/tournaments/{tournamentId}/games")]
    [ApiController]
    public class GamesController(ChampionshipContext context, IMapper mapper) : ControllerBase
    {

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames(int tournamentId)
        {
            if (!await context.Tournaments.AnyAsync(t => t.Id == tournamentId))
            {
                return NotFound();
            }
            return await context.Games.Where(g => g.TournamentId == tournamentId).ProjectTo<GameDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            var game = await context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return mapper.Map<GameDTO>(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            context.Entry(game).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            context.Games.Add(game);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return context.Games.Any(e => e.Id == id);
        }
    }
}
