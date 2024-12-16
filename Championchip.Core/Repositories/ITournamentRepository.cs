using Championchip.Core.Entities;
using Championchip.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Championchip.Core.Repositories
{
    public interface ITournamentRepository : IRepositoryBase<Tournament>
    {
        Task<PagedList<Tournament>> GetAllAsync(TournamentRequestParams  CompanyRequestParams);
        Task<PagedList<Tournament>> GetAllAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  CompanyRequestParams);
        Task<Tournament?> GetAsync(int id, TournamentRequestParams requestParams);
        Task<Tournament?> GetAsync(Expression<Func<Tournament, bool>> conditions, TournamentRequestParams  CompanyRequestParams );
    }
}
