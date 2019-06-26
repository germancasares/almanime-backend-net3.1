using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly CloudBlobContainer avatarContainer;

        public ImageRepository(IConfiguration configuration)
        {
            if (!CloudStorageAccount.TryParse(configuration.GetConnectionString("AzureStorage"), out var storageAccount)) throw new InvalidOperationException("AzureStorage connection string not set.");

            var cloudBlobClient = storageAccount.CreateCloudBlobClient();

            avatarContainer = cloudBlobClient.GetContainerReference("avatars");
        }

        public async Task<string> UploadAvatar(IFormFile avatar, Guid userID)
        {
            await avatarContainer.CreateIfNotExistsAsync();

            var ext = Path.GetExtension(avatar.FileName);

            var cloudBlockBlob = avatarContainer.GetBlockBlobReference($"{userID}{ext}");
            await cloudBlockBlob.UploadFromStreamAsync(avatar.OpenReadStream());

            return cloudBlockBlob.Uri.AbsoluteUri; 
        }
    }
}
