using System;

namespace TheNeoGeoArchive.WebApp.ViewModels
{
    public class PlatformViewModel
    {
        public Guid PlatformId { set; get; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public int? Generation { get; set; }
        public string? Type { get; set; }
        public PlatformReleaseViewModel? Release { get; set; }
        public int? Discontinued { get; set; }
        public decimal? IntroductoryPrice { get; set; }
        public int? UnitsSold { get; set; }
        public string? Media { get; set; }
        public string? Cpu { get; set; }
        public string? Memory { get; set; }
        public string? Display { get; set; }
    }

    public class PlatformReleaseViewModel
    {
        public DateTime? Japan { set; get; }
        public DateTime? NorthAmerica { set; get; }
        public DateTime? Europe { set; get; }
    }
}
