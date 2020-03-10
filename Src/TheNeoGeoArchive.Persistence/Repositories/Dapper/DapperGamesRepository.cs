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

                var _ = await connection.ExecuteAsync(InsertCmdText, game);
                return game.GameId;
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Game>(SelectQueryText, new { });
            }
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<Game>(SelectQueryText, new { @id = gameId });
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
                    return new Game
                    {
                        GameId = result.game_id,
                        Name = result.name,
                        Developer = result.developer,
                        Genre = result.genre,
                        Modes = result.modes,
                        Publisher = result.publisher,
                        Series = result.series,
                        Title = result.title,
                        Year = result.year,
                        Release = new Release
                        {
                            Aes = result.release_aes,
                            Mvs = result.release_mvs,
                            Cd = result.release_cd
                        }
                    };
                }
            }
        }

        #region [ Command Text ]

        private const string InsertCmdText = @"INSERT INTO games(
	            game_id, name, title, genre, modes, series, developer, publisher, year, release_mvs, release_aes, release_cd)
            VALUES(@GameId, @Name, @Title, @Genre, @Modes, @Series, @Developer, @Publisher, @Year, null, null, null);";

        private const string SelectQueryText = @"SELECT
	            game_id as GameId, name, title, genre, modes, series, developer, publisher, year, release_mvs, release_aes, release_cd
            FROM games;";

        private const string SelectOneQueryText = @"SELECT
	            game_id as GameId, name, title, genre, modes, series, developer, publisher, year, release_mvs, release_aes, release_cd
            FROM games
            WHERE game_id = @id;";

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