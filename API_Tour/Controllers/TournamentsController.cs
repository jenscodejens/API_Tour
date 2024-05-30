using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentRepository _repository;

        public TournamentsController(ITournamentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            var tournaments = await _repository.GetAllAsync();

            if (!tournaments.Any())
            {
                return NotFound("No tournaments found");
            }

            return Ok(tournaments);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _repository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound("Tournament not found");
            }

            return tournament;
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (!await _repository.AnyAsync(id))
            {
                return NotFound();
            }
            _repository.Update(tournament);

            return NoContent();
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _repository.Add(tournament);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _repository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound("Tournament not found");
            }

            _repository.Remove(tournament);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
