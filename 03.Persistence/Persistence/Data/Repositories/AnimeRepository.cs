using Domain.Enums;
using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class AnimeRepository : BaseRepository<Anime>, IAnimeRepository
    {
        public AnimeRepository(AlmanimeContext context) : base(context) { }

        public Anime GetByKitsuID(int kitsuID) => GetAll().SingleOrDefault(a => a.KitsuID == kitsuID);

        public Anime GetBySlug(string slug) => GetAll().SingleOrDefault(a => a.Slug == slug);

        public int GetAnimesInSeason(int year, ESeason season) => GetAll().Count(a => a.StartDate.Year == year && a.Season == season);
        public IQueryable<Anime> GetSeason(int year, ESeason season) => GetAll().Where(a => a.StartDate.Year == year && a.Season == season);

        public IQueryable<Anime> GetByFansub(string acronym) => GetAll().Where(a => a.Episodes.Any(e => e.Subtitles.Any(s => s.Fansub.Acronym == acronym && s.Status == ESubtitleStatus.Published)));

        public IQueryable<Anime> GetCompletedByFansub(string acronym) => GetAll().Where(a => a.Episodes.Any(e => e.Subtitles.Any(s => s.Fansub.Acronym == acronym && s.Status == ESubtitleStatus.Published)));
    }
}
