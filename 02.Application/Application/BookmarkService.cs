using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookmarkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetByUserID(Guid userID) => _unitOfWork.Bookmarks.GetByUserID(userID).Select(b => b.Anime.Slug);

        public Bookmark Create(string slug, Guid identityID)
        {
            var anime = _unitOfWork.Animes.GetBySlug(slug);
            if (anime == null) throw new ArgumentException(nameof(slug));

            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            var bookmark = _unitOfWork.Bookmarks.Get(slug, user.ID);
            if (bookmark != null) throw new ArgumentException($"{nameof(slug)} & {nameof(identityID)}");

            bookmark = _unitOfWork.Bookmarks.Create(new Bookmark
            {
                AnimeID = anime.ID,
                UserID = user.ID,
            });

            _unitOfWork.Save();

            return bookmark;
        }

        public void Delete(string animeSlug, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            var bookmark = _unitOfWork.Bookmarks.Get(animeSlug, user.ID);
            if (bookmark == null) throw new ArgumentException($"{nameof(animeSlug)} & {nameof(identityID)}");

            _unitOfWork.Bookmarks.Delete(bookmark);
            _unitOfWork.Save();
        }
    }
}
