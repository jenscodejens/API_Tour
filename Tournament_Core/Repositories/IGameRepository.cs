﻿using Tournament_Core.Entities;

namespace Tournament_Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Game tournament);
        void Update(Game tournament);
        void Remove(Game tournament);
    }
}

