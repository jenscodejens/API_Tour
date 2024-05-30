using Tournament_Core.Repositories;
using Tournament_Data.Data;

namespace Tournament_Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TourDbContext _context;
        public ITournamentRepository TournamentRepository => new TournamentRepository(_context);    

        public IGameRepository GameRepository => new GameRepository(_context);

        public UoW(TourDbContext context)
        {
            this._context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
