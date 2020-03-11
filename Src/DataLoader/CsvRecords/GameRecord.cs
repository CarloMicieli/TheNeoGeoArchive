using System;

namespace DataLoader.CsvRecords
{
    public class GameRecord
    {
        public Guid? GameId { set; get; }
        public string Name { set; get; }
        public string Title { set; get; }
        public string Genre { set; get; }
        public string Modes { set; get; }
        public string Series { set; get; }
        public string Developer { set; get; }
        public string Publisher { set; get; }
        public int? Year { set; get; }
        public string MVS { set; get; }
        public string AES { set; get; }
        public string CD { set; get; }

        public DateTime? MVSDateTime()
        {
            if (DateTime.TryParse(MVS, out var dt))
                return dt;

            return null;
        }

        public DateTime? CDDateTime()
        {
            if (DateTime.TryParse(CD, out var dt))
                return dt;

            return null;
        }

        public DateTime? AESDateTime()
        {
            if (DateTime.TryParse(AES, out var dt))
                return dt;

            return null;
        }
    }
}