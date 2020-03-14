using System;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;

namespace TheNeoGeoArchive.IntegrationTests.SeedData
{
    public class PlatformsSeedData
    {
        public static void SeedPlatforms(IPlatformsRepository platformsRepository)
        {
            platformsRepository.CreatePlatform(new Platform
            {
                PlatformId = new Guid("c101d369-cf37-4850-87ec-9866be46f812"),
                Name = "Neo Geo AES",
                Slug = "neogeo",
                Manufacturer = "SNK Corporation",
                Generation = 4,
                Type = "Home video game console",
                Release = new PlatformRelease
                {
                    Japan = new DateTime(1990, 4, 26),
                    NorthAmerica = new DateTime(1990, 8, 22),
                    Europe = new DateTime(1991, 1, 1)
                },
                Discontinued = 1997,
                IntroductoryPrice = 649.99M,
                UnitsSold = 1000000,
                Media = "ROM cartridge",
                Cpu = "Motorola 68000 @ 12MHz, Zilog Z80A @ 4MHz",
                Memory = "64KB RAM, 84KB VRAM, 2KB Sound Memory",
                Display = "320×224 resolution, 4096 on-screen colors out of a palette of 65536"
            });
        }
    }
}
