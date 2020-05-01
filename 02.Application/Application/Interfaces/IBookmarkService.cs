using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IBookmarkService
    {
        Bookmark Create(string slug, Guid identityID);
        void Delete(string animeSlug, Guid identityID);
        IEnumerable<string> GetByUserID(Guid userID);
    }
}
