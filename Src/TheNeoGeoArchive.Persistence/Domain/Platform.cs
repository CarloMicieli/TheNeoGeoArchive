using System;

namespace TheNeoGeoArchive.Persistence.Domain
{
    public sealed class Platform
    {
        public Guid PlatformId { set; get; }
        public string Name { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public int? Generation { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public DateTime? Discontinued { get; set; }
        public decimal? IntroductoryPrice { get; set; }
        public int? UnitsSold { get; set; }
    }
}