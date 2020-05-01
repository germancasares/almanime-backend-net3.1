using Persistence.Data.Repositories.Interfaces;
using System;

namespace Persistence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlmanimeContext _context;

        public UnitOfWork(
            AlmanimeContext context,

            IAnimeRepository animes,
            IBookmarkRepository bookmarks,
            IEpisodeRepository episodes,
            IFansubRepository fansubs,
            IMembershipRepository memberships,
            IStorageRepository storage,
            ISubtitleRepository subtitles,
            ISubtitlePartialRepository subtitlesPartials,
            IUserRepository users
            )
        {
            _context = context;

            Animes = animes;
            Bookmarks = bookmarks;
            Episodes = episodes;
            Fansubs = fansubs;
            Memberships = memberships;
            Storage = storage;
            Subtitles = subtitles;
            SubtitlePartials = subtitlesPartials;
            Users = users;
        }

        public IAnimeRepository Animes { get; }
        public IBookmarkRepository Bookmarks { get; }
        public IEpisodeRepository Episodes { get; }
        public IFansubRepository Fansubs { get; }
        public IMembershipRepository Memberships { get; }
        public IStorageRepository Storage { get; }
        public ISubtitleRepository Subtitles { get; set; }
        public ISubtitlePartialRepository SubtitlePartials { get; set; }
        public IUserRepository Users { get; }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save() => _context.SaveChanges();
    }
}
