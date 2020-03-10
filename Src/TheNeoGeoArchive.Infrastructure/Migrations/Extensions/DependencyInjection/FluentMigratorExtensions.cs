using System;
using System.Linq;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace TheNeoGeoArchive.Infrastructure.Migrations.Extensions.DependencyInjection
{
    public static class FluentMigratorExtensions
    {
        public static IServiceCollection ReplaceMigrations(this IServiceCollection services, Action<MigrationOptions> options)
        {
            var descriptors = services
                .Where(d => d.ServiceType == typeof(IMigrationProcessor) || d.ServiceType == typeof(IMigrationGenerator))
                .ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            return AddMigrations(services, options);
        }

        public static IServiceCollection AddMigrations(this IServiceCollection services, Action<MigrationOptions> options)
        {
            MigrationOptions migrationOptions = new MigrationOptions();
            options?.Invoke(migrationOptions);

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder => builder.ConfigureRunner(migrationOptions)
                                                   .ScanIn(typeof(InitialMigration).Assembly).For.Migrations()) //TODO
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }
    }
}