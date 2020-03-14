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
        public async Task SendPlatforms(IEnumerable<PlatformRecord> platformRecords)
        {
            var sent = 0;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using (var http = new HttpClient(handler))
            {
                foreach (var platformRecord in platformRecords)
                {
                    decimal? price = null;
                    if (platformRecord.IntroductoryPrice != null &&
                        decimal.TryParse(platformRecord.IntroductoryPrice.Replace("$", ""), out var p))
                    {
                        price = p;
                    }

                    Console.WriteLine("Sending {0}", platformRecord.Name);
                    try
                    {
                        var platform  = new
                        {
                            platformRecord.PlatformId,
                            platformRecord.Name,
                            platformRecord.Slug,
                            platformRecord.Manufacturer,
                            platformRecord.Generation,
                            platformRecord.Type,
                            Release = new 
                            {
                                Japan = platformRecord.ReleaseJp,
                                NorthAmerica = platformRecord.ReleaseNa,
                                Europe = platformRecord.ReleaseEu
                            },
                            platformRecord.Discontinued,
                            platformRecord.UnitsSold,
                            IntroductoryPrice = price,
                            platformRecord.Media,
                            platformRecord.Cpu,
                            platformRecord.Sound,
                            platformRecord.Memory,
                            platformRecord.Display
                        };
                        var json = JsonSerializer.Serialize(platform);

                        Console.WriteLine(platform);

                        var respone = await http.PostAsync("https://localhost:5001/api/v1/platforms",
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

                Console.WriteLine("{0} platform(s) sent", sent);
            }
        }

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
                        gameRecord.GameId ??= Guid.NewGuid();

                        var game = new
                        {
                            gameRecord.GameId,
                            gameRecord.Name,
                            gameRecord.Title,
                            gameRecord.Genre,
                            gameRecord.Modes,
                            gameRecord.Series,
                            gameRecord.Developer,
                            gameRecord.Publisher,
                            gameRecord.Year,
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