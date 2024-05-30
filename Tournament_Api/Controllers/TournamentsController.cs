using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController(IUoW uoW) : ControllerBase
    {
        private readonly IUoW uoW = uoW;

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            var tournaments = await uoW.TournamentRepository.GetAllAsync();

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
            var tournament = await uoW.TournamentRepository.GetAsync(id);

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

            if (!await uoW.TournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }
            uoW.TournamentRepository.Update(tournament);

            return NoContent();
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            uoW.TournamentRepository.Add(tournament);
            await uoW.CompleteAsync();

            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await uoW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound("Tournament not found");
            }

            uoW.TournamentRepository.Remove(tournament);
            await uoW.CompleteAsync();

            return NoContent();
        }
    }
}
