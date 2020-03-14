using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;

namespace TheNeoGeoArchive.GrpcServices.Services
{
    public class GamesService : Games.GamesBase
    {
        private readonly ILogger<GamesService> _logger;       
        private readonly IGamesRepository _repo;

        public GamesService(ILogger<GamesService> logger, IGamesRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public override async Task<CreateGameReply> CreateGame(CreateGameRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Create new game {Name}", request.Name);
            
            var newId = await _repo.Create(new Game 
            {
                GameId = new Guid(request.GameId),
                Name = request.Name,
                Title = request.Title,
                Publisher = request.Publisher,
                Developer = request.Developer,
                Genre = request.Genre,
                Modes = request.Modes,
                Series = request.Series,
                Year = request.Year,
                Release = new Release
                {
                    Aes = new DateTime(request.Relase.Aes),
                    Mvs = new DateTime(request.Relase.Mvs),
                    Cd = new DateTime(request.Relase.Cd)
                }
            });  
            
            return new CreateGameReply
            {
                NewId = newId.ToString()
            };
        }
    }
}