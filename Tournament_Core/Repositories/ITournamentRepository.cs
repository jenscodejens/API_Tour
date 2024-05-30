using Tournament_Core.Entities;

namespace Tournament_Core.Repositories
{
    //TODO: Borde kunna använda IRepository<T> eller IRepository<T> : class, gör klart basic först
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournament tournament);
        void Update(Tournament tournament);
        void Remove(Tournament tournament);
    }
}

