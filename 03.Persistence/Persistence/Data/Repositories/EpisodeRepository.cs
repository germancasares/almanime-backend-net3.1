using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(AlmanimeContext context) : base(context) { }

        public Episode GetByAnimeSlugAndNumber(string animeSlug, int number) => GetAll().SingleOrDefault(e => e.Anime.Slug == animeSlug && e.Number == number);
        public Episode GetByAnimeIDAndNumber(Guid guid, int number) => GetAll().SingleOrDefault(e => e.Anime.ID == guid && e.Number == number);
        public IQueryable<Episode> GetByFansub(string acronym) => GetAll().Where(e => e.Subtitles.Any(s => s.Fansub.Acronym == acronym));
        public IQueryable<Episode> GetByAnimeID(Guid guid) => GetAll().Where(e => e.Anime.ID == guid);
        public IQueryable<Episode> GetByAnimeSlug(string animeSlug) => GetAll().Where(e => e.Anime.Slug == animeSlug);
    }
}
