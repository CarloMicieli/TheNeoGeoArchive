using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataLoader.CsvRecords;
using Grpc.Net.Client;
using TheNeoGeoArchive.GrpcServices;

namespace DataLoader
{
    public class GrpcClient
    {
        public async Task SendGames(IEnumerable<GameRecord> gameRecords)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =  HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Games.GamesClient(channel);

            foreach (var gameRecord in gameRecords)
            {
                var reply = await client.CreateGameAsync(new CreateGameRequest 
                {
                    GameId = gameRecord.GameId.ToString(),
                    Developer = gameRecord.Developer,
                    Genre = gameRecord.Genre,
                    Modes = gameRecord.Modes,
                    Name = gameRecord.Name,
                    Publisher = gameRecord.Publisher,
                    Series = gameRecord.Series,
                    Title = gameRecord.Title,
                    Year = gameRecord.Year ?? 1900,
                    Relase = new CreateGameRequest.Types.Release 
                    {
                        Mvs = gameRecord.MVSDateTime()?.Ticks ?? 0,
                        Aes = gameRecord.AESDateTime()?.Ticks ?? 0,
                        Cd = gameRecord.CDDateTime()?.Ticks ?? 0
                    }
                });
            }
        }
    }
}