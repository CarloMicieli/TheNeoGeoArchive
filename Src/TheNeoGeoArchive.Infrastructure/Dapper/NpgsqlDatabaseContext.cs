using System.Data.Common;
using Microsoft.Extensions.Options;
using Npgsql;

namespace TheNeoGeoArchive.Infrastructure.Dapper
{
    public class NpgsqlDatabaseContext : IDatabaseContext
    {
        private readonly DatabaseOptions _options;

        public NpgsqlDatabaseContext(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public DbConnection NewConnection()
        {
            return new NpgsqlConnection(_options.ConnectionString);
        }
    }
}