using Domain.Models;
using System;
using System.Linq;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IBookmarkRepository : IBaseRepository<Bookmark>
    {
        Bookmark Get(string animeSlug, Guid userID);
        IQueryable<Bookmark> GetByUserID(Guid userID);
    }
}
