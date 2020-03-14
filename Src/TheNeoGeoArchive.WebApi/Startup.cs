using System;
using System.Text.Json;
using AutoMapper;
using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TheNeoGeoArchive.Infrastructure.Dapper.Extensions.DependencyInjection;
using TheNeoGeoArchive.Infrastructure.Migrations.Extensions.DependencyInjection;
using TheNeoGeoArchive.Persistence.Extensions.DependencyInjection;
using TheNeoGeoArchive.WebApi.DependencyInjection;
using TheNeoGeoArchive.WebApi.ViewModels.Validators;

namespace TheNeoGeoArchive.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GameViewModelValidator>());

            services.AddOpenApi();
            services.AddVersioning();

            services.AddApiVersioning();

            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDapper(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanTypeHandlersIn(typeof(GuidTypeHandler).Assembly);
            });

            services.AddMigrations(options =>
            {
                options.UsePostgres(connectionString);
            });

            services.AddRepositories();
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Run database migration
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
