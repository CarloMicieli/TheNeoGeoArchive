using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;
using TheNeoGeoArchive.WebApi.ViewModels;

namespace TheNeoGeoArchive.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _repo;

        public GamesController(IMapper mapper, IGamesRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var game = await _repo.GetGameById(id);
            if (game is null)
                return NotFound();

            var viewModel = _mapper.Map<GameViewModel>(game);
            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _repo.GetAll();
            return Ok(games.Select(g => _mapper.Map<GameViewModel>(g)).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGame(GameViewModel newGame)
        {
            var game = _mapper.Map<Game>(newGame);
            var newId = await _repo.Create(game);
            return Ok(newId);
        }
    }
}