using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheNeoGeoArchive.Infrastructure.Dapper.Extensions.DependencyInjection;
using TheNeoGeoArchive.Infrastructure.Migrations.Extensions.DependencyInjection;
using FluentMigrator.Runner;

namespace TheNeoGeoArchive.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
            where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                string connectionString = "Data Source=test.db";

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

                    Console.WriteLine(runner.GetType().ToString());

                    try
                    {
                        // Seed the database with test data.
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
}