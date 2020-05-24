using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Collections.Generic;
using TheNeoGeoArchive.WebApp;

namespace TheNeoGeoArchive.IntegrationTests.WebApis
{
    public class GamesApiTests : AbstractWebApplicationFixture
    {
        public GamesApiTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_GetGame_ByName()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/games/fatfury1");

            response.EnsureSuccessStatusCode();

            var content = await ExtractContent<GetGameByNameResponse>(response);

            content.Name.Should().Be("fatfury1");
        }

        [Fact]
        public async Task Should_GetGameByName_ReturnNotFoundWhenGameDoesNotExist()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/games/notfound");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_GetGame_ById()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/games/id/b0b576da-9ede-4d39-8a21-1970988af58c");

            response.EnsureSuccessStatusCode();

            var content = await ExtractContent<GetGameByNameResponse>(response);

            content.Name.Should().Be("fatfury1");
        }

        [Fact]
        public async Task Should_GetGameById_ReturnNotFoundWhenGameDoesNotExist()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/v1/games/id/{Guid.Empty}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_CreateNewGame_ValidateRequests()
        {
            var client = CreateHttpClient();

            var request = JsonContent(new { });

            var response = await client.PostAsync($"/api/v1/games", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = await AsBadRequest(response);
            content.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_CreateNewGame()
        {
            var client = CreateHttpClient();

            var request = JsonContent(new
            {
                GameId = new Guid("007e60d2-32b9-4808-b993-a33ee98cfdc9"),
                Name = "sonicwi2",
                Title = "Aero Fighters 2",
                Genre = "Shoot 'em up",
                Modes = "Single-player, Multiplayer",
                Series = "Aero Fighters",
                Developer = "Video System",
                Publisher = "SNK, Video System",
                Year = 1994,
                Release = new
                {
                    Mvs = new DateTime(1994, 7, 18),
                    Aes = new DateTime(1994, 8, 26),
                    Cd = new DateTime(1994, 9, 29),
                }
            });

            var response = await client.PostAsync($"/api/v1/games", request);

            response.EnsureSuccessStatusCode();
        }
    }

    public sealed class GetGameByNameResponse
    {
        public Guid GameId { set; get; }
        public string Name { set; get; } = null!;
        public string Title { set; get; } = null!;
        public string Genre { set; get; } = null!;
        public string Modes { set; get; } = null!;
        public string Series { set; get; }
        public string Developer { set; get; }
        public string Publisher { set; get; }
        public int? Year { set; get; }
        public ReleaseResponse Release { set; get; }
    }

    public sealed class ReleaseResponse
    {
        public DateTime? Mvs { set; get; }
        public DateTime? Aes { set; get; }
        public DateTime? Cd { set; get; }
    }    
}
