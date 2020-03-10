using Microsoft.Extensions.DependencyInjection;
using TheNeoGeoArchive.Persistence.Repositories;
using TheNeoGeoArchive.Persistence.Repositories.Dapper;

namespace TheNeoGeoArchive.Persistence.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGamesRepository, DapperGamesRepository>();
            services.AddScoped<IPlatformsRepository, DapperPlatformsRepository>();
            return services;
        }
    }
}