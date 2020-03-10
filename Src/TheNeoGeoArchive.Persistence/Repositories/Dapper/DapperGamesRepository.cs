using Dapper;
using System;
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

        #region [ Command Text ]

        private const string InsertCmdText = @"INSERT INTO games(
	            game_id, title, genre, modes, series, developer, publisher, release, created_at, version)
            VALUES(@GameId, @Title, @Genre, @Modes, @Series, @Developer, @Publisher, @Release, null, 1);";

        private const string SelectQueryText = @"SELECT
	            game_id as GameId, title, genre, modes, series, developer, publisher, release, created_at, version
            FROM games;";

        private const string SelectOneQueryText = @"SELECT
	            game_id as GameId, title, genre, modes, series, developer, publisher, release, created_at, version
            FROM games
            WHERE game_id = @id;";

        #endregion  
    }
}