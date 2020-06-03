using Domain.Models;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IEpisodeRepository : IBaseRepository<Episode>
    {
        Episode GetByNumber(string animeSlug, int number);
        IQueryable<Episode> GetCompletedByFansub(string acronym);
    }
}
