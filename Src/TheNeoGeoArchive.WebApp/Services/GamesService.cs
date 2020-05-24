using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;

namespace TheNeoGeoArchive.WebApp.Services
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

        public override async Task GetGames(GetGamesRequest request, IServerStreamWriter<GameInfo> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Get all games");

            var games = await _repo.GetAll();
            foreach (var game in games)
            {
                await responseStream.WriteAsync(FromGame(game));
            }
        }

        public override async Task<GameInfo> GetGameByName(GetGameByNameRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get game {Name} info", request.Name);

            var game = await _repo.GetGameByName(request.Name);
            if (game is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
            }

            return FromGame(game);
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


        private static GameInfo FromGame(Game game)
        {
            return new GameInfo {
                GameId = game.GameId.ToString(),
                Name = game.Name,
                Title = game.Title,
                Modes = game.Modes,
                Publisher = game.Publisher,
                Developer = game.Developer,
                Genre = game.Genre,
                Series = game.Series,
                Year = game.Year ?? 0,
                Release = new GameRelease {
                    Aes = game.Release?.Aes?.Ticks ?? 0,
                    Mvs = game.Release?.Mvs?.Ticks ?? 0,
                    Cd = game.Release?.Cd?.Ticks ?? 0
                }
            };
        }
    }
}
