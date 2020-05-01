using Domain.Enums;
using Domain.Models;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IAnimeRepository : IBaseRepository<Anime>
    {
        int GetAnimesInSeason(int year, ESeason season);
        IQueryable<Anime> GetByFansub(string acronym);
        Anime GetByKitsuID(int kitsuID);
        Anime GetBySlug(string slug);
        IQueryable<Anime> GetSeason(int year, ESeason season);
    }
}
