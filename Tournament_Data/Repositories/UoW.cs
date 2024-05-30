using Tournament_Core.Repositories;
using Tournament_Data.Data;

namespace Tournament_Data.Repositories
{
    public class UoW(TourDbContext context) : IUoW
    {
        private readonly TourDbContext _context = context;
        public ITournamentRepository TournamentRepository => new TournamentRepository(_context);    

        public IGameRepository GameRepository => new GameRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
