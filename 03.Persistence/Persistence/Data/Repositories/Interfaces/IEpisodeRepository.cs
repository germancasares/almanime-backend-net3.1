using Domain.Models;
<<<<<<< HEAD
=======
using System;
>>>>>>> persistence: Add methods to EpisodeRepository
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IEpisodeRepository : IBaseRepository<Episode>
    {
        Episode GetByAnimeIDAndNumber(Guid guid, int number);
        Episode GetByAnimeSlugAndNumber(string animeSlug, int number);
        IQueryable<Episode> GetCompletedByFansub(string acronym);
        IQueryable<Episode> GetByAnimeID(Guid guid);
        IQueryable<Episode> GetByAnimeSlug(string animeSlug);
    }
}
