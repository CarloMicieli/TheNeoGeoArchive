using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;
using TheNeoGeoArchive.WebApp.ViewModels;

namespace TheNeoGeoArchive.WebApp.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _repo;

        public GamesController(IMapper mapper, IGamesRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var game = await _repo.GetGameById(id);
            if (game is null)
                return NotFound();

            var viewModel = _mapper.Map<GameViewModel>(game);
            return Ok(viewModel);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGameByName(string name)
        {
            var game = await _repo.GetGameByName(name);
            if (game is null)
                return NotFound();

            var viewModel = _mapper.Map<GameViewModel>(game);
            return Ok(viewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _repo.GetAll();
            return Ok(games.Select(g => _mapper.Map<GameViewModel>(g)).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewGame(GameViewModel newGame)
        {
            var game = _mapper.Map<Game>(newGame);
            var newId = await _repo.Create(game);
            return Ok(newId);
        }
    }
}