using AutoMapper;
using TheNeoGeoArchive.Persistence.Domain;
using TheNeoGeoArchive.WebApi.ViewModels;

namespace TheNeoGeoArchive.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GameViewModel, Game>();
            CreateMap<Game, GameViewModel>();
            CreateMap<PlatformViewModel, Platform>();
            CreateMap<Platform, PlatformViewModel>();
            CreateMap<Release, ReleaseViewModel>();
            CreateMap<ReleaseViewModel, Release>();
        }
    }
}