using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNeoGeoArchive.Infrastructure.Dapper;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories.Dapper
{
    public class DapperPlatformsRepository : IPlatformsRepository
    {
        private readonly IDatabaseContext _dbContext;

        public DapperPlatformsRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreatePlatform(Platform p)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();

                var _ = await connection.ExecuteAsync(InsertGameCommand, new
                {
                    p.PlatformId,
                    p.Name,
                    p.Slug,
                    p.Manufacturer,
                    p.Generation,
                    ReleasedJp = p.Release?.Japan,
                    ReleasedEu = p.Release?.Europe,
                    ReleasedNa = p.Release?.NorthAmerica,
                    PlatformType = p.Type,
                    p.Discontinued,
                    p.IntroductoryPrice,
                    p.UnitsSold,
                    p.Media,
                    p.Cpu,
                    p.Memory,
                    p.Display
                });
                return p.PlatformId;
            }
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<PlatformDto>(GetAllPlatformsQuery, new { });
                return results.Select(dto => NewPlatformFromDto(dto));
            }
        }

        public async Task<Platform?> GetByPlatformSlug(string slug)
        {
            await using (var connection = _dbContext.NewConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<PlatformDto>(GetPlatformBySlugQuery, new { slug });
                if (result is null)
                {
                    return null;
                }
                else
                {
                    return NewPlatformFromDto(result);
                }
            }
        }

        private class PlatformDto
        {
            public Guid platform_id { set; get; }
            public string name { get; set; } = null!;
            public string slug { get; set; } = null!;
            public string manufacturer { get; set; } = null!;
            public int? generation { get; set; }
            public string? platform_type { get; set; }
            public DateTime? released_jp { get; set; }
            public DateTime? released_na { get; set; }
            public DateTime? released_eu { get; set; }
            public int? discontinued { get; set; }
            public decimal? introductory_price { get; set; }
            public int? units_sold { get; set; }
            public string? media { get; set; }
            public string? cpu { get; set; }
            public string? memory { get; set; }
            public string? display { get; set; }
        }

        private static Platform NewPlatformFromDto(PlatformDto dto)
        {
            return new Platform
            {
                PlatformId = dto.platform_id,
                Name = dto.name,
                Slug = dto.slug,
                Manufacturer = dto.manufacturer,
                Generation = dto.generation,
                Discontinued = dto.discontinued,
                IntroductoryPrice = dto.introductory_price,
                UnitsSold = dto.units_sold,
                Media = dto.media,
                Type = dto.platform_type,
                Cpu = dto.cpu,
                Memory = dto.memory,
                Display = dto.display,
                Release = new PlatformRelease
                {
                    Europe = dto.released_eu,
                    NorthAmerica = dto.released_na,
                    Japan = dto.released_jp
                }
            };
        }

        #region [ Commands/Queries text ]

        private const string GetAllPlatformsQuery = "SELECT * FROM platforms;";
        private const string GetPlatformBySlugQuery = "SELECT * FROM platforms WHERE slug = @slug";

        private const string InsertGameCommand = @"INSERT INTO platforms(
                platform_id, name, slug, manufacturer, generation, released_jp, released_eu, released_na, platform_type,
                discontinued, introductory_price, units_sold, media, cpu, memory, display)
            VALUES(
                @PlatformId, @Name, @Slug, @Manufacturer, @Generation, @ReleasedJp, @ReleasedEu, @ReleasedNa, @PlatformType,
                @Discontinued, @IntroductoryPrice, @UnitsSold, @Media, @Cpu, @Memory, @Display);";

        #endregion 
    }
}