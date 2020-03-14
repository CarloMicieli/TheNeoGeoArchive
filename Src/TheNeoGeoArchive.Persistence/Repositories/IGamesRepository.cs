using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories
{
    public interface IGamesRepository
    {
        Task<Game?> GetGameById(Guid gameId);

        Task<Game?> GetGameByName(string name);

        Task<IEnumerable<Game>> GetAll();

        Task<Guid> Create(Game game);
    }
}