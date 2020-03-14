using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using DataLoader.CsvRecords;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace DataLoader
{
    enum LoadModes 
    {
        Grpc,
        Rest,
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<LoadModes>(
                    "--mode",
                    getDefaultValue: () => LoadModes.Rest,
                    description: "Data loading mode (rest or grpc)")
            };
            rootCommand.Description = "My loading data CLI tool";

            rootCommand.Handler = CommandHandler.Create<LoadModes>(async mode => {
                var gameRecords = LoadRecords<GameRecord>("games.csv");
                var platformRecords = LoadRecords<PlatformRecord>("platforms.csv");

                if (mode == LoadModes.Grpc)
                {
                    await LoadWithGrpc(gameRecords, platformRecords);
                }
                else
                {
                    await LoadWithWebApi(gameRecords, platformRecords);
                }
            });

            await rootCommand.InvokeAsync(args);
        }

        static async Task LoadWithWebApi(IEnumerable<GameRecord> gameRecords, IEnumerable<PlatformRecord> platformRecords)
        {
            var apiClient = new WebApiClient();
            await apiClient.SendGames(gameRecords);
            await apiClient.SendPlatforms(platformRecords);
        }

        static async Task LoadWithGrpc(IEnumerable<GameRecord> gameRecords, IEnumerable<PlatformRecord> platformRecords)
        {
            var grpcClient = new GrpcClient();
            await grpcClient.SendGames(gameRecords);
        }

        static IEnumerable<TRecord> LoadRecords<TRecord>(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TRecord>().ToList();
            }
        }
    }
}