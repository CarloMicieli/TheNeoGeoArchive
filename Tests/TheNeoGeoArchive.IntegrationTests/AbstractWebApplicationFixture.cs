using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using TheNeoGeoArchive.IntegrationTests.Responses;
using TheNeoGeoArchive.WebApp;

namespace TheNeoGeoArchive.IntegrationTests
{
    public abstract class AbstractWebApplicationFixture : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        protected AbstractWebApplicationFixture(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient()
        {
            return _factory.CreateClient();
        }

        protected HttpContent JsonContent(object model)
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }

        protected async Task<TContent> ExtractContent<TContent>(HttpResponseMessage response)
        {
            var s = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TContent>(s, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        protected Task<ErrorResponse> AsError(HttpResponseMessage response)
        {
            return ExtractContent<ErrorResponse>(response);
        }

        protected Task<BadRequestResponse> AsBadRequest(HttpResponseMessage response)
        {
            return ExtractContent<BadRequestResponse>(response);
        }
    }
}
