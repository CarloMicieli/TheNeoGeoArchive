using System;

namespace TheNeoGeoArchive.Persistence.Domain
{
    public class GameInfo
    {
        public Guid GameId { set; get; }
        public string Name { set; get; } = null!;
        public string Title { set; get; } = null!;
    }
}
