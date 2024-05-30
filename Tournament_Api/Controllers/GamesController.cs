using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Repositories;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IUoW uoW) : ControllerBase
    {
        private readonly IUoW uoW = uoW;

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var games = await uoW.GameRepository.GetAllAsync();

            if (!games.Any())
            {
                return NotFound("No games found");
            }

            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await uoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            return Ok(game);
        }

        // GET: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            if (!await uoW.GameRepository.AnyAsync(id))
            {
                return NotFound();
            }
            uoW.GameRepository.Update(game);
            await uoW.CompleteAsync();

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            bool tournamentExists = await uoW.GameRepository.AnyAsync(game.TournamentId);

            if (!tournamentExists)
            {
                return BadRequest();
            }

            uoW.GameRepository.Add(game);
            await uoW.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await uoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            uoW.GameRepository.Remove(game);
            await uoW.CompleteAsync();

            return NoContent();
        }
    }
}
