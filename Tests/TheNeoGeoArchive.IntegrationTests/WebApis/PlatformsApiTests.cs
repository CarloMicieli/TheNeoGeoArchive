using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TheNeoGeoArchive.WebApi;
using Xunit;

namespace TheNeoGeoArchive.IntegrationTests.WebApis
{
    public class PlatformsApiTests : AbstractWebApplicationFixture
    {
        public PlatformsApiTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_GetPlatform_BySlug()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/platforms/neogeo");
            response.EnsureSuccessStatusCode();

            var content = await ExtractContent<GetPlatformResponse>(response);

            content.Slug.Should().Be("neogeo");
        }

        [Fact]
        public async Task Should_GetPlatformBySlug_ReturnsNotFound_WhenPlatformDoesNotExist()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/api/v1/platforms/notfound");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_PostPlatform_ValidateRequest()
        {
            var client = CreateHttpClient();

            var content = JsonContent(new { });

            var response = await client.PostAsync("/api/v1/platforms", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_PostPlatform_CreateANewPlatform()
        {
            var client = CreateHttpClient();

            var content = JsonContent(new
            {
                platformId = Guid.NewGuid().ToString(),
                name = "Neo Geo 2",
                slug = "neogeo2",
                manufacturer = "SNK Corporation",
                generation = 4,
                type = "Home video game console",
                release = new
                {
                    japan = "1990-04-26T00:00:00",
                    northAmerica = "1990-08-22T00:00:00",
                    europe = "1991-01-01T00:00:00"
                },
                discontinued = 1997,
                introductoryPrice = 649,
                unitsSold = 1000000,
                media = "ROM cartridge",
                cpu = "Motorola 68000 @ 12MHz, Zilog Z80A @ 4MHz",
                memory = "64KB RAM, 84KB VRAM, 2KB Sound Memory",
                display = "320×224 resolution, 4096 on-screen colors out of a palette of 65536"
            });

            var response = await client.PostAsync("/api/v1/platforms", content);

            response.EnsureSuccessStatusCode();
        }

        private class GetPlatformResponse
        {
            public string Slug { set; get; }
        }
               
        private async Task EnsureStatusCode(HttpResponseMessage response, HttpStatusCode status)
        {
            var error = await AsError(response);
            error.Should().Be(null);
            response.StatusCode.Should().Be(status);
        }
    }
}
