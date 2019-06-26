using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<string> UploadAvatar(IFormFile avatar, Guid userID);
    }
}
