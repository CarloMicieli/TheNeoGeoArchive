using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories
{
    public interface IPlatformsRepository
    {
        Task<Guid> CreatePlatform(Platform newPlatform);
        
        Task<Platform?> GetByPlatformSlug(string slug);

        Task<IEnumerable<Platform>> GetAllPlatforms();
    }
}