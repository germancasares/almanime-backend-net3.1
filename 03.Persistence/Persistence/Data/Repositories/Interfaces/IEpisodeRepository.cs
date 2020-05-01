using Domain.Models;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IEpisodeRepository : IBaseRepository<Episode>
    {
        Episode GetByNumber(string animeSlug, int number);
    }
}
