using System;

namespace TheNeoGeoArchive.Persistence.Domain
{
    public class Game
    {
        public Guid GameId { set; get; }
        public string Name { set; get; } = null!;
        public string Title { set; get; } = null!;
        public string Genre { set; get; } = null!;
        public string Modes { set; get; } = null!;
        public string? Series { set; get; }
        public string? Developer { set; get; }
        public string? Publisher { set; get; }
        public int? Year { set; get; }
        public Release? Release { set; get; }
    }
}