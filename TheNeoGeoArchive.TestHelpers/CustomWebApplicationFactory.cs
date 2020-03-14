using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheNeoGeoArchive.Infrastructure.Dapper.Extensions.DependencyInjection;
using TheNeoGeoArchive.Infrastructure.Migrations.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using TheNeoGeoArchive.IntegrationTests.SeedData;
using TheNeoGeoArchive.Persistence.Repositories;
using System.IO;

namespace TheNeoGeoArchive.TestHelpers
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IDisposable
            where TStartup : class
    {
        private readonly Guid contextId;

        public CustomWebApplicationFactory()
        {
            this.contextId = Guid.NewGuid();
        }

        public new void Dispose()
        {
            File.Delete($"{contextId}.db");
            base.Dispose();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                string connectionString = $"Data Source={contextId}.db";

                // Replace with sqlite
                services.ReplaceDapper(options =>
                {
                    options.UseSqlite(connectionString);
                });

                services.ReplaceMigrations(options =>
                {
                    options.UseSqlite(connectionString);
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var runner = scopedServices.GetRequiredService<IMigrationRunner>();
                    runner.MigrateUp();

                    try
                    {
                        // Seed the database with test data.
                        GamesSeedData.SeedGames(scopedServices.GetRequiredService<IGamesRepository>());
                        PlatformsSeedData.SeedPlatforms(scopedServices.GetRequiredService<IPlatformsRepository>());
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
#pragma warning restore CA1063 // Implement IDisposable Correctly
}