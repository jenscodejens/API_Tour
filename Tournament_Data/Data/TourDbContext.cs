using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;

namespace Tournament_Data.Data
{
    public class TourDbContext(DbContextOptions<TourDbContext> options) : DbContext(options)
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Games)
                .WithOne()
                .HasForeignKey(g => g.TournamentId)
                .IsRequired();
        }
    }
}
