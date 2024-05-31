using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Dto;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tournament_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUoW _uoW;

        public GamesController(IUoW uoW, IMapper mapper)
        {
            _mapper = mapper;
            _uoW = uoW;
        }
        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
        {
            var games = await _uoW.GameRepository.GetAllAsync();

            if (!games.Any())
            {
                return NotFound("No games found");
            }

            //return Ok(_mapper.Map<IEnumerable<GameDto>>(games)); mindre effektivt, hämtar mapping oavsett om det finns spel eller inte
            var gamesDto = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(gamesDto);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _uoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            var gameDto = _mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }

        // GET: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);

            if (id != game.Id)
            {
                return BadRequest();
            }

            if (!await _uoW.GameRepository.AnyAsync(id))
            {
                return NotFound("Game not found");
            }

            _uoW.GameRepository.Update(game);
            await _uoW.CompleteAsync();

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);

            bool tournamentExists = await _uoW.GameRepository.AnyAsync(game.TournamentId);

            if (!tournamentExists)
            {
                return BadRequest();
            }

            _uoW.GameRepository.Add(game);
            await _uoW.CompleteAsync();

            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, _mapper.Map<GameDto>(game));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            _uoW.GameRepository.Remove(game);
            await _uoW.CompleteAsync();

            var gameDto = _mapper.Map<GameDto>(game);

            return Ok(gameDto);
        }
    }
}
