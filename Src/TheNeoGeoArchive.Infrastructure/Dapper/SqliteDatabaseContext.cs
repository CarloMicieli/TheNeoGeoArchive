using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace TheNeoGeoArchive.Infrastructure.Dapper
{
    public class SqliteDatabaseContext : IDatabaseContext
    {
        private readonly DatabaseOptions _options;

        public SqliteDatabaseContext(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public DbConnection NewConnection()
        {
            return new SqliteConnection(_options.ConnectionString);
        }
    }
}