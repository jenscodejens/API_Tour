﻿using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;

namespace Tournament_Data.Repositories
{
    public class GameRepository(TourDbContext context) : IGameRepository
    {
        private readonly TourDbContext _context = context;

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game?> GetAsync(int id)
        {
            var result = await _context.Games.FindAsync(id);

            if (result == null)
            {
                return null;
            }

            return result;
        }


        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
        }
    }
}
