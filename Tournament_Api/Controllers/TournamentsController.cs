using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Dto;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUoW _uoW;

        public TournamentsController(IUoW uoW, IMapper mapper)
        {
            _mapper = mapper;
            _uoW = uoW;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _uoW.TournamentRepository.GetAllAsync();

            if (!tournaments.Any())
            {
                return NotFound("No tournaments found");
            }

            var tournamentsDto = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(tournamentsDto);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _uoW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound("Tournament not found");
            }

            var tournamentsDto = _mapper.Map<TournamentDto>(tournament);
            return Ok(tournamentsDto);
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, TournamentDto tournamentDto)
        {
            var tournament = _mapper.Map<Tournament>(tournamentDto);

            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (!await _uoW.TournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }
            _uoW.TournamentRepository.Update(tournament);
            await _uoW.CompleteAsync();

            return NoContent();
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(TournamentDto tournamentDto)
        {
            var tournament = _mapper.Map<Tournament>(tournamentDto);

            _uoW.TournamentRepository.Add(tournament);
            await _uoW.CompleteAsync();

            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, _mapper.Map<TournamentDto>(tournament));          
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _uoW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound("Tournament not found");
            }

            _uoW.TournamentRepository.Remove(tournament);
            await _uoW.CompleteAsync();

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);

            return Ok(tournamentDto);
        }
    }
}
