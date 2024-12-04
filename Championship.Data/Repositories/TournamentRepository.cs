using AutoMapper;
using Championchip.Core.DTOs;
using Championchip.Core.Entities;
using Championchip.Core.Repositories;
using Championship.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public class TournamentRepository(ChampionshipContext context) : RepositoryBase<Tournament>(context), ITournamentRepository
    {
    }
}
