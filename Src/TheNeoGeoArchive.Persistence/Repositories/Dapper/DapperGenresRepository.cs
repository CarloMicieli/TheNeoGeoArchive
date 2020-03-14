using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNeoGeoArchive.Infrastructure.Dapper;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories.Dapper
{
    public class DapperGenresRepository : IGenresRepository
    {
        private readonly IDatabaseContext _dbContext;

        public DapperGenresRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dictionary<string, IEnumerable<GameInfo>>> GetAllGenres()
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<GameGenreDto>(GetAllGenresQuery, new { });

                return results
                    .Select(row => new
                    {
                        Genre = row.genre,
                        Info = new GameInfo
                        {
                            GameId = row.game_id,
                            Name = row.name,
                            Title = row.title
                        }
                    })
                    .GroupBy(it => it.Genre)
                    .ToDictionary(it => it.Key, it => it.Select(y => y.Info));
            }
        }

        public async Task<(string, IEnumerable<GameInfo>)?> GetGamesByGenre(string genre)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<GameGenreDto>(GetAllGamesByGenreQuery, new { genre });
                if (results.Count() == 0)
                    return null;

                return results
                    .GroupBy(it => it.genre)
                    .Select(it => (it.Key, it.Select(row => new GameInfo
                    {
                        GameId = row.game_id,
                        Name = row.name,
                        Title = row.title
                    }).ToList()))
                    .FirstOrDefault();
            }
        }

        #region [ Query text ]

        private const string GetAllGenresQuery = @"SELECT game_id, name, title, genre FROM games;";

        private const string GetAllGamesByGenreQuery = @"SELECT game_id, name, title, genre FROM games WHERE genre = @genre;";

        #endregion

        private class GameGenreDto
        {
            public Guid game_id { set; get; } = Guid.Empty;
            public string name { set; get; } = "";
            public string title { set; get; } = "";
            public string genre { set; get; } = "";
        }
    }
}
