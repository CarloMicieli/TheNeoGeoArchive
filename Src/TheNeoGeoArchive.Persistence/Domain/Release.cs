using System;

namespace TheNeoGeoArchive.Persistence.Domain
{
    public sealed class Release
    {
        public DateTime? Mvs { set; get; }
        public DateTime? Aes { set; get; }
        public DateTime? Cd { set; get; }
    }
}