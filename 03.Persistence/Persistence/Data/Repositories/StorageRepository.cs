using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Persistence.Data.Repositories.Interfaces;
using System;
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

        public async void DeleteAvatar(Guid userID) => await Delete("avatars", $"users/{userID}/avatar");
        public async Task<string> UploadAvatar(IFormFile avatar, Guid userID)
        {
            return await Upload("avatars", $"users/{userID}/avatar{avatar.GetExtension()}", avatar);
        }

        public async void DeleteSubtitle(Guid fansubID, Guid subtitleID) => await Delete("subtitles", $"fansub/{fansubID}/subtitles/{subtitleID}");
        public async Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID)
        {
            return await Upload("subtitles", $"fansub/{fansubID}/subtitles/{subtitleID}{subtitle.GetExtension()}", subtitle);
        }

        public async void DeleteSubtitlePartial(Guid fansubID, Guid subtitleID, Guid subtitlePartialID) => await Delete("subtitles", $"fansub/{fansubID}/subtitles/{subtitleID}/{subtitlePartialID}");
        public async Task<string> UploadSubtitlePartial(IFormFile subtitlePartial, Guid fansubID, Guid subtitleID, Guid subtitlePartialID)
        {
            return await Upload("subtitles", $"fansub/{fansubID}/subtitles/{subtitleID}/{subtitlePartialID}{subtitlePartial.GetExtension()}", subtitlePartial);
        }

        private async Task Delete(string container, string blob)
        {
            var subtitlesContainer = _cloudBlobClient.GetContainerReference(container);

            var cloudBlockBlob = subtitlesContainer.GetBlockBlobReference(blob);
            await cloudBlockBlob.DeleteAsync();
        }

        private async Task<string> Upload(string container, string blob, IFormFile file)
        {
            var subtitlesContainer = _cloudBlobClient.GetContainerReference(container);

            await subtitlesContainer.CreateIfNotExistsAsync();

            var cloudBlockBlob = subtitlesContainer.GetBlockBlobReference(blob);
            await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return cloudBlockBlob.Uri.AbsoluteUri;
        }
    }
}
