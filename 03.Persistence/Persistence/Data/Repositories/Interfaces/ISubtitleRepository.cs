using Domain.Models;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface ISubtitleRepository : IBaseRepository<Subtitle>
    {
        IQueryable<Subtitle> GetByFansubAndAnime(string fansubAcronym, string animeSlug);
    }
}
