using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(AlmanimeContext context) : base(context) { }

        public Episode GetByNumber(string animeSlug, int number) => GetAll().SingleOrDefault(e => e.Anime.Slug == animeSlug && e.Number == number);
    }
}
