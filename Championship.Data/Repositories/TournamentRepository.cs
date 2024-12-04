using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championship.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class TournamentRepository(ChampionshipContext context) : ITournamentRepository
    {
        public void Add(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tournament>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournament> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
