using System;

namespace TheNeoGeoArchive.WebApi.ViewModels
{
    public class GameItemViewModel
    {
        public Guid GameId { set; get; }
        public string Name { set; get; } = null!;
        public string Title { set; get; } = null!;
    }
}
