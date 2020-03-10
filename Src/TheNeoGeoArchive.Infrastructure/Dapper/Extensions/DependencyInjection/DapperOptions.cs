using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TheNeoGeoArchive.Infrastructure.Dapper.Extensions.DependencyInjection
{
    public sealed class DapperOptions
    {
        private readonly IServiceCollection services;
        private readonly List<Assembly> assemblies;

        internal DapperOptions(IServiceCollection services)
        {
            this.services = services;
            this.assemblies = new List<Assembly>();
        }

        public void UsePostgres(string connectionString)
        {
            ConfigureDatabaseOptions(connectionString);
            services.AddScoped<IDatabaseContext, NpgsqlDatabaseContext>();
        }

        public void UseSqlite(string connectionString)
        {
            ConfigureDatabaseOptions(connectionString);
            services.AddScoped<IDatabaseContext, SqliteDatabaseContext>();
        }

        public void ScanTypeHandlersIn(Assembly assembly)
        {
            this.assemblies.Add(assembly);
        }

        public void ScanTypeHandlersIn(params Assembly[] assemblies)
        {
            if (assemblies.Length > 0)
            {
                this.assemblies.AddRange(assemblies);
            }
        }

        internal List<Assembly> Assemblies
        {
            get
            {
                return assemblies;
            }
        }

        private void ConfigureDatabaseOptions(string connectionString)
        {
            services.Configure<DatabaseOptions>(myOptions =>
            {
                myOptions.ConnectionString = connectionString;
            });
        }
    }
}