using Dapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNeoGeoArchive.Infrastructure.Dapper;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories.Dapper
{
    public class DapperGamesRepository : IGamesRepository
    {
        private readonly IDatabaseContext _dbContext;

        public DapperGamesRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Create(Game game)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();

                var _ = await connection.ExecuteAsync(InsertCmdText, new
                {
                    game.GameId,
                    game.Name,
                    game.Title,
                    game.Genre,
                    game.Modes,
                    game.Series,
                    game.Developer,
                    game.Publisher,
                    game.Year,
                    ReleaseMvs = game.Release?.Mvs,
                    ReleaseAes = game.Release?.Aes,
                    ReleaseCd = game.Release?.Cd
                });
                return game.GameId;
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<GamesDto>(SelectQueryText, new { });
                return results.Select(dto => NewGameFromDto(dto));
            }
        }

        public async Task<Game?> GetGameById(Guid gameId)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<GamesDto>(SelectOneQueryText, new { @id = gameId });
                if (result is null)
                {
                    return null;
                }
                else
                {
                    return NewGameFromDto(result);
                }
            }
        }

        public async Task<Game?> GetGameByName(string name)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<GamesDto>(SelectByNameText, new { @name = name });
                if (result is null)
                {
                    return null;
                }
                else
                {
                    return NewGameFromDto(result);
                }
            }
        }

        private Game NewGameFromDto(GamesDto dto)
        {
            return new Game
            {
                GameId = dto.game_id,
                Name = dto.name,
                Developer = dto.developer,
                Genre = dto.genre,
                Modes = dto.modes,
                Publisher = dto.publisher,
                Series = dto.series,
                Title = dto.title,
                Year = dto.year,
                Release = new Release
                {
                    Aes = dto.release_aes,
                    Mvs = dto.release_mvs,
                    Cd = dto.release_cd
                }
            };
        }

        #region [ Command Text ]

        private const string InsertCmdText = @"INSERT INTO games(
	            game_id, name, title, genre, modes, series, developer, publisher, year, release_mvs, release_aes, release_cd)
            VALUES(@GameId, @Name, @Title, @Genre, @Modes, @Series, @Developer, @Publisher, @Year, @ReleaseMvs, @ReleaseAes, @ReleaseCd);";

        private const string SelectQueryText = @"SELECT * FROM games;";

        private const string SelectOneQueryText = @"SELECT * FROM games WHERE game_id = @id;";

        private const string SelectByNameText = @"SELECT * FROM games WHERE name = @name;";

        #endregion  
    }

    internal class GamesDto
    {
        public Guid game_id { set; get; } = Guid.Empty;
        public string name { set; get; } = "";
        public string title { set; get; } = "";
        public string genre { set; get; } = "";
        public string modes { set; get; } = "";
        public string? series { set; get; }
        public string? developer { set; get; }
        public string? publisher { set; get; }
        public int? year { set; get; }
        public DateTime? release_mvs { set; get; }
        public DateTime? release_aes { set; get; }
        public DateTime? release_cd { set; get; }
    }
}