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

        //[FunctionName("StoreProfileImage")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            [Blob("sample-images-sm", FileAccess.Write)] CloudBlobContainer cloudBlobContainer,
            [AccessToken] ClaimsPrincipal principal)
        {
            await cloudBlobContainer.CreateIfNotExistsAsync();

            var identityID = principal.Claims.GetIdentityID();
            var user = _userService.GetByIdentityID(identityID);
            user.AvatarUrl = "";
        }
    }
}
