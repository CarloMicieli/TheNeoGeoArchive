using Microsoft.Extensions.DependencyInjection;

namespace TheNeoGeoArchive.WebApi.DependencyInjection
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "NeoGeo archive API";
                    document.Info.Description = "A web API for the best gaming console ever";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Carlo Micieli",
                        Email = string.Empty,
                        Url = "https://carlomicieli.github.io"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            return services;
        }
    }
}