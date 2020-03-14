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
            CreateMap<Release, ReleaseViewModel>();
            CreateMap<ReleaseViewModel, Release>();

            CreateMap<PlatformViewModel, Platform>();
            CreateMap<Platform, PlatformViewModel>();
            CreateMap<PlatformRelease, PlatformReleaseViewModel>();
            CreateMap<PlatformReleaseViewModel, PlatformRelease>();
        }
    }
}