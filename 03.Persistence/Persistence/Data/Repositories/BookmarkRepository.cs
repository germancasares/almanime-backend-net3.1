using Domain.Models;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Linq;

namespace Persistence.Data.Repositories
{
    public class BookmarkRepository : BaseRepository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(AlmanimeContext context) : base(context) { }

        public IQueryable<Bookmark> GetByUserID(Guid userID) => GetAll().Where(b => b.UserID == userID);

        public Bookmark Get(string animeSlug, Guid userID) => GetByUserID(userID).SingleOrDefault(b => b.Anime.Slug == animeSlug);
    }
}
