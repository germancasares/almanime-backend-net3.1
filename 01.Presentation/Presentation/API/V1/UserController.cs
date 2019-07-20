using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Presentation.ActionFilters;

namespace Presentation.API.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpPut("self")]
        public async Task Update(UserDTO userDTO)
        {
            var identityID = User.Claims.GetIdentityID();

            await _userService.Update(userDTO, identityID);
        }
    }
}
