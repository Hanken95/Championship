using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Championchip.Core.Entities;

namespace Championship.Data.Data
{
    public class ChampionshipContext(DbContextOptions<ChampionshipContext> options) : DbContext(options)
    {
        public DbSet<Tournament> Tournaments { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
    }
}
