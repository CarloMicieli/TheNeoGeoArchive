using System;

namespace TheNeoGeoArchive.WebApi.ViewModels
{
    public sealed class GameViewModel
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
        public ReleaseViewModel? Release { set; get; }
    }

    public sealed class ReleaseViewModel
    {
        public DateTime? Mvs { set; get; }
        public DateTime? Aes { set; get; }
        public DateTime? Cd { set; get; }
    }
}