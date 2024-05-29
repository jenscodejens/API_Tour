using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;

namespace Tournament_Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TourDbContext _context;

        public TournamentRepository(TourDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments.Include(t => t.Games).ToListAsync();
        }

        public async Task<Tournament?> GetAsync(int id)
        {
            var result = await _context.Tournaments
               .Include(t => t.Games)
               .FirstOrDefaultAsync(t => t.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"No tournament found with Id: {id}");
            }

            return result;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournaments.AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Entry(tournament).State = EntityState.Modified;
        }

        public void Remove(Tournament tournament)
        {
            _context.Tournaments.Remove(tournament);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}