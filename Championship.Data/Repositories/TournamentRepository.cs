using Championchip.Core.DTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championchip.Core.Request;
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
        public async Task<PagedList<Tournament>> GetAllAsync(TournamentRequestParams  requestParams)
        {
            var tournaments = requestParams.IncludeGames ? DbSet.Include(t => t.Games).AsQueryable() :
                                                           DbSet.AsQueryable();

            return await PagedList<Tournament>.CreateAsync(tournaments, requestParams.PageNumber, requestParams.PageSize);

        }
        public async Task<PagedList<Tournament>> GetAllAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  requestParams)
        {
            var tournaments = requestParams.IncludeGames ? DbSet.Where(conditions).Include(t => t.Games).AsQueryable() :
                                                           DbSet.Where(conditions).AsQueryable();

            return await PagedList<Tournament>.CreateAsync(tournaments, requestParams.PageNumber, requestParams.PageSize);

        }
        public async Task<Tournament?> GetAsync(int id, TournamentRequestParams requestParams)
        {
            return await GetAsync(t => t.Id == id, requestParams);
        }
        public async Task<Tournament?> GetAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams requestParams )
        {
            if(requestParams.IncludeGames) return await DbSet.Include(t => t.Games).FirstOrDefaultAsync(conditions);

            return await DbSet.FirstOrDefaultAsync(conditions);
        }
    }
}
