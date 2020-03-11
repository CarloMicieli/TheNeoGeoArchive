using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using DataLoader.CsvRecords;

namespace DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var gameRecords = LoadRecords<GameRecord>("games.csv");
            var apiClient = new WebApiClient();

            await apiClient.SendGames(gameRecords);
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