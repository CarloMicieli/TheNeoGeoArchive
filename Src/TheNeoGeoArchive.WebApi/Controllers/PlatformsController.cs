using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;
using TheNeoGeoArchive.WebApi.ViewModels;

namespace TheNeoGeoArchive.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlatformsRepository _repo;

        public PlatformsController(IMapper mapper, IPlatformsRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(PlatformViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGameByName(string slug)
        {
            var platform = await _repo.GetByPlatformSlug(slug);
            if (platform is null)
                return NotFound();

            var viewModel = _mapper.Map<PlatformViewModel>(platform);
            return Ok(viewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PlatformViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPlatforms()
        {
            var platforms = await _repo.GetAllPlatforms();
            return Ok(platforms.Select(p => _mapper.Map<PlatformViewModel>(p)).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewPlatform(PlatformViewModel newPlatform)
        {
            var platform = _mapper.Map<Platform>(newPlatform);
            var newId = await _repo.CreatePlatform(platform);
            return Ok(newId);
        }
    }
}
