using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IStorageRepository
    {
        Task<string> UploadAvatar(IFormFile avatar, Guid userID);
        Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID);
    }
}
