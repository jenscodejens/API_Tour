using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using Tournament_Data.Repositories;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _repository;

        public GamesController(IGameRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var games = await _repository.GetAllAsync();
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _repository.GetAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            return Ok(game);
        }

        // GET: api/Games/5
        // TODO: check that TournamentId exists too
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame([FromBody] int id)
        {
            var game = await _repository.GetAsync(id);

            if (id != game.Id && !GameExists(id).Result)
            {
                return BadRequest();
            }

            _repository.Update(game);
            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame([FromBody] Game game)
        {
            var tournamentExists = await _repository.AnyAsync(game.TournamentId);

            if (!tournamentExists)
            {
                return BadRequest("No tournament with that Id");
            }

            _repository.Add(game);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _repository.GetAsync(id);
            
            if (game == null)
            {
                return NotFound();
            }

            _repository.Remove(game);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GameExists(int id)
        {
            return await _repository.AnyAsync(id);
        }
    }
}
