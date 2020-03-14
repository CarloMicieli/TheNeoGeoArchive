using System;

namespace TheNeoGeoArchive.Persistence.Domain
{
    public sealed class Platform
    {
        public Guid PlatformId { set; get; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public int? Generation { get; set; }
        public string? Type { get; set; }
        public PlatformRelease? Release { get; set; }
        public int? Discontinued { get; set; }
        public decimal? IntroductoryPrice { get; set; }
        public int? UnitsSold { get; set; }
        public string? Media { get; set; }
        public string? Cpu { get; set; }
        public string? Memory { get; set; }
        public string? Display { get; set; }
    }

    public sealed class PlatformRelease
    {
        public DateTime? Japan { set; get; }
        public DateTime? NorthAmerica { set; get; }
        public DateTime? Europe { set; get; }
    }
}