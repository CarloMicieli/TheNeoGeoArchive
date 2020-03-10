using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CsvHelper;

namespace DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var http = new HttpClient())
            using (var reader = new StreamReader("games.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var gameRecords = csv.GetRecords<GameRecord>();
                //  Console.WriteLine("{0} game(s) found", gameRecords.Count());

                var sent = 0;
                foreach (var gameRecord in gameRecords)
                {
                    Console.WriteLine("Sending {0}", gameRecord.Title);

                    try
                    {
                        gameRecord.GameId = gameRecord.GameId ?? Guid.NewGuid();

                        var json = JsonSerializer.Serialize(gameRecord);
                        var respone = await http.PostAsync("https://localhost:5001/api/games",
                            new StringContent(json, Encoding.UTF8, "application/json"));
                        if (respone.IsSuccessStatusCode)
                        {
                            sent++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine("{0} game(s) sent", sent);
            }
        }
    }

    class GameRecord
    {
        public Guid? GameId { set; get; }
        public string Title { set; get; }
        public string Genre { set; get; }
        public string Modes { set; get; }
        public string Series { set; get; }
        public string Developer { set; get; }
        public string Publisher { set; get; }
        public int? Release { set; get; }
        public string MVS { set; get; }
        public string AES { set; get; }
        public string CD { set; get; }
    }

}