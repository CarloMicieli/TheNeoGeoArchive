using System.Data.Common;

namespace TheNeoGeoArchive.Infrastructure.Dapper
{
    public interface IDatabaseContext
    {
        DbConnection NewConnection();
    }
}