using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private class GetPlatformResponse
        {
            public string Slug { set; get; }
        }
    }
}
