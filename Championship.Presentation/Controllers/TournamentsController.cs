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
using Bogus.DataSets;
using Championchip.Core.Repositories;
using Championchip.Core.DTOs.TournamentDTOs;
using Championchip.Core.DTOs.GameDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Championship.Presentation.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController(IMapper mapper, IUnitOfWork unit) : ControllerBase
    {

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournament(bool includeGames)
        {
            if (!await unit.TournamentRepository.AnyAsync()) return NotFound("No tournaments in the database");

            return Ok(mapper.Map<IEnumerable<TournamentDTO>>(await unit.TournamentRepository.GetAllAsync(includeGames)));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id, bool includeGames)
        {

            var tournament = await unit.TournamentRepository.GetAsync(t => t.Id == id, includeGames);

            if (tournament == null)
            {
                return NotFound($"No tournament with id {id} in the database");
            }

            return Ok(mapper.Map<TournamentDTO>(tournament));
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTournament(int id, TournamentUpdateDTO dto)
        {
            var existingTournament = await unit.TournamentRepository.GetAsync(t => t.Id == id);
            if (existingTournament == null)
            {
                return NotFound("Tournament not found");
            }

            mapper.Map(dto, existingTournament);

            try
            {
                await unit.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unit.TournamentRepository.AnyAsync(e => e.Id == id))
                {
                    return StatusCode(500, "Could not be saved to the databse");
                }
                else
                {
                    throw;
                }
            }

            return Ok(mapper.Map<TournamentDTO>(existingTournament));
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> PostTournament(TournamentCreateDTO dto)
        {
            if (dto == null) return BadRequest("Incorrect input");
            Tournament tournament = mapper.Map<Tournament>(dto);
            unit.TournamentRepository.Add(tournament);
            await unit.CompleteAsync();

            var createdTournament = mapper.Map<TournamentDTO>(tournament);

            return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await unit.TournamentRepository.GetAsync(t => t.Id == id);
            if (tournament == null)
            {
                return NotFound("Tournament not found in database");
            }

            unit.TournamentRepository.Remove(tournament);
            await unit.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchTournament(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            var tournamentToPatch = await unit.TournamentRepository.GetAsync(g => g.Id == id);
            if (tournamentToPatch == null)
            {
                return NotFound($"There is no tournament with id: {id}");
            }

            var dto = mapper.Map<TournamentUpdateDTO>(tournamentToPatch);

            patchDocument.ApplyTo(dto);

            mapper.Map(dto, tournamentToPatch);
            await unit.CompleteAsync();

            return NoContent();
        }
    }
}
