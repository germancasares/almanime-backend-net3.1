using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Jobs.Security;
using System.Security.Claims;
using Application.Interfaces;
using Infrastructure.Helpers;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Net.Http;
using System.Drawing;

namespace Jobs.Functions
{
    public class StoreProfileImage
    {
        private readonly IUserService _userService;

        public StoreProfileImage(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("StoreProfileImage")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            [Blob("sample-images-sm", FileAccess.Write)] CloudBlobContainer cloudBlobContainer,
            [AccessToken] ClaimsPrincipal principal)
        {
            await cloudBlobContainer.CreateIfNotExistsAsync();

            var identityID = principal.Claims.GetIdentityID();
            var user = _userService.GetByIdentityID(identityID);

            // Get request body

            IFormFile algo;

            //dynamic data = await req.Content.ReadAsAsync<object>();

            //// Set name to query string or body data
            //var name = data?.path;

            //Image img = Image.FromFile(name);















            // Create a file in your local MyDocuments folder to upload to a blob.
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";
            var sourceFile = Path.Combine(localPath, localFileName);
            // Write text to the file.
            File.WriteAllText(sourceFile, "Hello, World!");

            Console.WriteLine("Temp file = {0}", sourceFile);
            Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);

            // Get a reference to the blob address, then upload the file to the blob.
            // Use the value of localFileName for the blob name.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
            await cloudBlockBlob.UploadFromFileAsync(sourceFile);
        }
    }
}
