using System;

namespace TheNeoGeoArchive.WebApi.ViewModels
{
    public sealed class GameViewModel
    {
        public Guid GameId { set; get; }
        public string Title { set; get; } = null!;
        public string Genre { set; get; } = null!;
        public string Modes { set; get; } = null!;
        public string? Series { set; get; } 
        public string? Developer { set; get; } 
        public string? Publisher { set; get; } 
        public int? Release { set; get; } 
    }
}