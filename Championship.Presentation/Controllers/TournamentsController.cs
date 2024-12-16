using Microsoft.AspNetCore.Mvc;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championchip.Core.DTOs.TournamentDTOs;
using Microsoft.AspNetCore.JsonPatch;
using System.Data.Entity.Infrastructure;
using Service.Contracts;
using Championchip.Core.Request;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Championship.Presentation.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController(IServiceManager manager) : ControllerBase
    {

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournaments([FromQuery]TournamentRequestParams requestParams)
        {
            if (!await manager.TournamentService.AnyAsync()) return NotFound("No tournaments in the database");

            (IEnumerable<TournamentDTO> companyDtos, MetaData metaData) result = await manager.TournamentService.GetAllAsync(requestParams);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return base.Ok(result.companyDtos);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id, [FromQuery] TournamentRequestParams requestParams)
        {
            if (!await manager.TournamentService.AnyAsync(t => t.Id == id))
            {
                return NotFound($"There is no tournament with id: {id}");
            }

            return Ok(await manager.TournamentService.GetAsync(t => t.Id == id, requestParams));
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTournament(int id, TournamentUpdateDTO dto)
        {
            if (!await manager.TournamentService.AnyAsync(t => t.Id == id))
            {
                return NotFound($"There is no tournament with id: {id}");
            }
            try
            {
                await manager.TournamentService.PutAsync(id, dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await manager.TournamentService.AnyAsync(e => e.Id == id))
                {
                    return StatusCode(500, "Could not be saved to the databse");
                }
                else
                {
                    throw;
                }
            }

            return Ok(manager.TournamentService.GetAsync(id));
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> PostTournament(TournamentCreateDTO dto)
        {
            if (dto == null) return BadRequest("Incorrect input");

            var createdTournament = await manager.TournamentService.AddAsync(dto);

            return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            if (!await manager.TournamentService.AnyAsync(t => t.Id == id))
            {
                return NotFound($"There is no tournament with id: {id}");
            }

            await manager.TournamentService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchTournament(int id, JsonPatchDocument<TournamentUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            if (!await manager.TournamentService.AnyAsync(t => t.Id == id))
            {
                return NotFound($"There is no tournament with id: {id}");
            }

            await manager.TournamentService.PatchAsync(id, patchDocument);

            return NoContent();
        }
    }
}
