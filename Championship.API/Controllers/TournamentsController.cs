﻿using System;
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
using Bogus.DataSets;

namespace Championship.API.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController(ChampionshipContext context, IMapper mapper) : ControllerBase
    {

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournament(bool includeGames)
        {
            return Ok(includeGames ? mapper.Map<IEnumerable<TournamentDTO>>(await context.Tournaments.Include(t => t.Games).ToListAsync()) :
                                     mapper.Map<IEnumerable<TournamentDTO>>(await context.Tournaments.ToListAsync()));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return mapper.Map<TournamentDTO>(tournament);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingTournament = await context.Tournaments.FindAsync(id);
            if (existingTournament == null)
            {
                return NotFound();
            }

            mapper.Map(dto, existingTournament);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
                {
                    return NotFound();
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
            Tournament tournament = mapper.Map<Tournament>(dto);
            context.Tournaments.Add(tournament);
            await context.SaveChangesAsync();

            var createdTournament = mapper.Map<TournamentDTO>(tournament);

            return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            context.Tournaments.Remove(tournament);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool TournamentExists(int id)
        {
            return context.Tournaments.Any(e => e.Id == id);
        }
    }
}
