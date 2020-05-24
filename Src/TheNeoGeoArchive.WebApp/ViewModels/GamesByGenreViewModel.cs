using System.Collections.Generic;

namespace TheNeoGeoArchive.WebApp.ViewModels
{
    public class GamesByGenreViewModel
    {
        public string? Genre { set; get; }
        public List<GameItemViewModel> Games { set; get; } = new List<GameItemViewModel>();
    }
}
