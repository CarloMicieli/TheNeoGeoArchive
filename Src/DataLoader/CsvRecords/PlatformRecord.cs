using System;

namespace DataLoader.CsvRecords
{
    public class PlatformRecord
    {
        public Guid PlatformId { set; get; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public int? Generation { get; set; }
        public string Type { get; set; }
        public DateTime? ReleaseJp { get; set; }
        public DateTime? ReleaseNa { get; set; }
        public DateTime? ReleaseEu { get; set; }
        public int? Discontinued { get; set; }
        public string IntroductoryPrice { get; set; }
        public int? UnitsSold { get; set; }
        public string Media { get; set; }
        public string Cpu { get; set; }
        public string Sound { get; set; }
        public string Memory { get; set; }
        public string Display { get; set; }
    }
}
