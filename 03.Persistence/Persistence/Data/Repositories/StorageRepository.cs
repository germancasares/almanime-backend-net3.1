using Domain.Models;
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
    public class StorageRepository : IStorageRepository
    {
        private readonly CloudBlobClient _cloudBlobClient;

        public StorageRepository(IConfiguration configuration)
        {
            if (!CloudStorageAccount.TryParse(configuration.GetConnectionString("AzureStorage"), out var storageAccount))
                throw new InvalidOperationException("AzureStorage connection string not set.");

            _cloudBlobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> UploadAvatar(IFormFile avatar, Guid userID)
        {
            var avatarContainer = _cloudBlobClient.GetContainerReference("avatars");

            await avatarContainer.CreateIfNotExistsAsync();

            var ext = Path.GetExtension(avatar.FileName);

            var cloudBlockBlob = avatarContainer.GetBlockBlobReference($"users/{userID}/avatar{ext}");
            await cloudBlockBlob.UploadFromStreamAsync(avatar.OpenReadStream());

            return cloudBlockBlob.Uri.AbsoluteUri; 
        }

        public async Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID)
        {
            var subtitlesContainer = _cloudBlobClient.GetContainerReference("subtitles");

            await subtitlesContainer.CreateIfNotExistsAsync();

            var cloudBlockBlob = subtitlesContainer.GetBlockBlobReference($"fansub/{fansubID}/subtitles/{subtitleID}.srt");
            await cloudBlockBlob.UploadFromStreamAsync(subtitle.OpenReadStream());

            return cloudBlockBlob.Uri.AbsoluteUri;
        }
    }
}
