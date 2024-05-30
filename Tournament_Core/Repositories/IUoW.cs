namespace Tournament_Core.Repositories
{
    public interface IUoW
    {
        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }
        Task CompleteAsync();
    }
}