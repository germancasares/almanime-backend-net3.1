using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IStorageRepository
    {
        void DeleteAvatar(Guid userID);
        void DeleteSubtitle(Guid fansubID, Guid subtitleID);
        void DeleteSubtitlePartial(Guid fansubID, Guid subtitleID, Guid subtitlePartialID);
        Task<string> UploadAvatar(IFormFile avatar, Guid userID);
        Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID);
        Task<string> UploadSubtitlePartial(IFormFile subtitlePartial, Guid fansubID, Guid subtitleID, Guid subtitlePartialID);
    }
}
