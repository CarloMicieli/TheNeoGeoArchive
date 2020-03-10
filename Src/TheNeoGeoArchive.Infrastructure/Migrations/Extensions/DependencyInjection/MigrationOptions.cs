namespace TheNeoGeoArchive.Infrastructure.Migrations.Extensions.DependencyInjection
{
    public class MigrationOptions
    {
        internal string? ConnectionString { get; set; }
        internal string? DriverName { get; set; }

        public void UsePostgres(string connectionString)
        {
            ConnectionString = connectionString;
            DriverName = "Postgres";
        }

        public void UseSqlite(string connectionString)
        {
            ConnectionString = connectionString;
            DriverName = "Sqlite";
        }
    }
}