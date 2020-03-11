using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataLoader.CsvRecords;

namespace DataLoader
{
    public sealed class WebApiClient
    {
        public async Task SendGames(IEnumerable<GameRecord> gameRecords)
        {
            var sent = 0;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using (var http = new HttpClient(handler))
            {
                foreach (var gameRecord in gameRecords)
                {
                    Console.WriteLine("Sending {0}", gameRecord.Title);
                    try
                    {
                        gameRecord.GameId = gameRecord.GameId ?? Guid.NewGuid();

                        var game = new
                        {
                            GameId = gameRecord.GameId,
                            Name = gameRecord.Name,
                            Title = gameRecord.Title,
                            Genre = gameRecord.Genre,
                            Modes = gameRecord.Modes,
                            Series = gameRecord.Series,
                            Developer = gameRecord.Developer,
                            Publisher = gameRecord.Publisher,
                            Year = gameRecord.Year,
                            Release = new
                            {
                                Mvs = gameRecord.MVSDateTime(),
                                Aes = gameRecord.AESDateTime(),
                                Cd = gameRecord.CDDateTime()
                            }
                        };
                        var json = JsonSerializer.Serialize(game);

                        Console.WriteLine(game);

                        var respone = await http.PostAsync("https://localhost:5001/api/v1/games",
                            new StringContent(json, Encoding.UTF8, "application/json"));
                        if (respone.IsSuccessStatusCode)
                        {
                            sent++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        Console.Error.WriteLine(ex.InnerException?.Message);
                    }
                }

                Console.WriteLine("{0} game(s) sent", sent);
            }
        }
    }
}