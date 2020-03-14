using System.Collections.Generic;
using System.Threading.Tasks;
using TheNeoGeoArchive.Persistence.Domain;

namespace TheNeoGeoArchive.Persistence.Repositories
{
    public interface IGenresRepository
    {
        Task<Dictionary<string, IEnumerable<GameInfo>>> GetAllGenres();

        Task<(string, IEnumerable<GameInfo>)?> GetGamesByGenre(string genre);
    }
}
