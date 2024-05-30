using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;

namespace Tournament_Data.Data
{
    public static class SeedData
    {
        public static async Task InitAsync(TourDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();

            // Create 3 tournaments, each with one game
            var seeds = new List<(string TournamentTitle, string GameTitle)>
            {
                ("Tournament 1", "Game 1"),
                ("Tournament 2", "Game 2"),
                ("Tournament 3", "Game 3")
            };

            foreach (var (tournamentTitle, gameTitle) in seeds)
            {
                var tournament = await context.Tournaments.FirstOrDefaultAsync(t => t.Title == tournamentTitle);
                if (tournament == null)
                {
                    tournament = new Tournament { Title = tournamentTitle, StartDate = DateTime.Now };
                    await context.Tournaments.AddAsync(tournament);
                    await context.SaveChangesAsync();
                }

                if (!context.Games.Any(g => g.Title == gameTitle && g.TournamentId == tournament.Id))
                {
                    var game = new Game { Title = gameTitle, Time = DateTime.Now, TournamentId = tournament.Id };
                    await context.Games.AddAsync(game);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
