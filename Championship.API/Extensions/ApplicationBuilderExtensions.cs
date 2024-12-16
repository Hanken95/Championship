using Championchip.Core.Entities;
using Championship.Data.Data;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace Championship.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<ChampionshipContext>();

                await db.Database.MigrateAsync();

                if (await db.Tournaments.AnyAsync()) return;

                var tournaments = GenerateTournaments(30);

                db.AddRange(tournaments);

                await db.SaveChangesAsync();
            }
        }

        private static List<Tournament> GenerateTournaments(int nrOfTournaments)
        {
            var faker = new Faker<Tournament>("sv").Rules((f, t) =>
            {
                t.StartDate = f.Date.Soon(5);
                t.Title = f.Vehicle.Type();
                t.Games = GenerateGames(f.Random.Int(4, 10), t.StartDate);
            });

            return faker.Generate(nrOfTournaments);
        }

        private static List<Game> GenerateGames(int nrOfGames, DateTime startOfTournament)
        {
            var faker = new Faker<Game>("sv").Rules((f, g) =>
            {
                g.Title = f.Vehicle.Fuel();
                g.Time = f.Date.Soon(2, startOfTournament);
            });

            return faker.Generate(nrOfGames);
        }
    }
}
