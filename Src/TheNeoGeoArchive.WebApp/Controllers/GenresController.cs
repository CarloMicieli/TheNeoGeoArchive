﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNeoGeoArchive.Persistence.Repositories;
using TheNeoGeoArchive.WebApp.ViewModels;

namespace TheNeoGeoArchive.WebApp.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _genresRepository;

        public GenresController(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GamesByGenreViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genresRepository.GetAllGenres();

            var content = genres.Select(it => new GamesByGenreViewModel
            {
                Genre = it.Key,
                Games = it.Value.Select(g => new GameItemViewModel
                {
                    GameId = g.GameId,
                    Name = g.Name,
                    Title = g.Title
                }).ToList()
            });

            return Ok(content);
        }

        [HttpGet("{genre}")]
        [ProducesResponseType(typeof(GamesByGenreViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGamesByGenre(string genre)
        {
            var result = await _genresRepository.GetGamesByGenre(genre);
            if (result is null)
            {
                return NotFound();
            }

            var content = new GamesByGenreViewModel
            {
                Genre = result.Value.Item1,
                Games = result.Value.Item2.Select(g => new GameItemViewModel
                {
                    GameId = g.GameId,
                    Name = g.Name,
                    Title = g.Title
                }).ToList()
            };

            return Ok(content);
        }
    }
}