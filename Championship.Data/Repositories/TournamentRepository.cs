﻿using AutoMapper;
using Championchip.Core.DTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championship.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class TournamentRepository(ChampionshipContext context) : RepositoryBase<Tournament>(context), ITournamentRepository
    {
        public async Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames)
        {
            if (includeGames)
            {
                return await DbSet.Include(t => t.Games).ToListAsync();
            }
            return await GetAllAsync();
        }
        public async Task<Tournament?> GetAsync(Expression<Func<Tournament, bool>> conditions, bool includeGames)
        {
            if(includeGames) return await DbSet.Include(t => t.Games).FirstOrDefaultAsync(conditions);

            return await DbSet.FirstOrDefaultAsync(conditions);
        }
    }
}
