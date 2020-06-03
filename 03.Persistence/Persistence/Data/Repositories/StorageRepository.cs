using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence.Data.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        public const string AvatarsBlobContainer = "avatars";
        public const string SubtitleBlobContainer = "subtitles";

        private readonly BlobServiceClient _blobServiceClient;

        public StorageRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureWebJobsStorage");

            if (connectionString == null)
                throw new InvalidOperationException("AzureWebJobsStorage connection string not set.");

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async void DeleteAvatar(Guid userID) => await Delete(AvatarsBlobContainer, $"users/{userID}/avatar", true);
        public async Task<string> UploadAvatar(IFormFile avatar, Guid userID)
        {
            return await Upload(AvatarsBlobContainer, $"users/{userID}/avatar{avatar.GetExtension()}", avatar);
        }

        public async void DeleteSubtitle(Guid fansubID, Guid subtitleID) => await Delete(SubtitleBlobContainer, $"fansub/{fansubID}/subtitles/{subtitleID}", true);
        public async Task<string> UploadSubtitle(IFormFile subtitle, Guid fansubID, Guid subtitleID)
        {
            return await Upload(SubtitleBlobContainer, $"fansub/{fansubID}/subtitles/{subtitleID}{subtitle.GetExtension()}", subtitle);
        }

        public async void DeleteSubtitlePartial(Guid fansubID, Guid subtitleID, Guid subtitlePartialID) => await Delete(SubtitleBlobContainer, $"fansub/{fansubID}/subtitles/{subtitleID}/{subtitlePartialID}", true);
        public async Task<string> UploadSubtitlePartial(IFormFile subtitlePartial, Guid fansubID, Guid subtitleID, Guid subtitlePartialID)
        {
            return await Upload(SubtitleBlobContainer, $"fansub/{fansubID}/subtitles/{subtitleID}/{subtitlePartialID}{subtitlePartial.GetExtension()}", subtitlePartial);
        }

        private async Task Delete(string reference, string blob, bool includeSnapshots = false)
        {
            var container = _blobServiceClient.GetBlobContainerClient(reference);

            var blobClient = container.GetBlobClient(blob);
            await blobClient.DeleteAsync(includeSnapshots ? DeleteSnapshotsOption.IncludeSnapshots : DeleteSnapshotsOption.None);
        }

        private async Task<string> Upload(string reference, string blob, IFormFile file)
        {
            var container = _blobServiceClient.GetBlobContainerClient(reference);

            await container.CreateIfNotExistsAsync();

            var blobClient = container.GetBlobClient(blob);
            await blobClient.UploadAsync(file.OpenReadStream());

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
