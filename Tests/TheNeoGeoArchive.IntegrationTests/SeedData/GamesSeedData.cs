using System;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.Persistence.Repositories;

namespace TheNeoGeoArchive.IntegrationTests.SeedData
{
    public static class GamesSeedData
    {
        public static void SeedGames(IGamesRepository gamesRepository)
        {
            gamesRepository.Create(new Game
            {
                GameId = new Guid("b0b576da-9ede-4d39-8a21-1970988af58c"),
                Name = "fatfury1",
                Title = "Fatal Fury: King of Fighters",
                Genre = "Fighting",
                Modes = "Single-player, Multiplayer",
                Series = "Fatal Fury",
                Developer = "SNK",
                Publisher = "SNK",
                Year = 1991,
                Release = new Release
                {
                    Mvs = new DateTime(1991, 11, 25),
                    Aes = new DateTime(1991, 12, 20),
                    Cd = new DateTime(1994, 09, 09)
                }
            });
        }
    }
}
