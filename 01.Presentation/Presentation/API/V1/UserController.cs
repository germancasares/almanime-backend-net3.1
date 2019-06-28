using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Presentation.ActionFilters;

namespace Presentation.API.V1
{
    [Authorize]
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(
            IMapper mapper,
            IUserService userService
            )
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPut("Self")]
        public async Task Update(UserDTO userDTO)
        {
            var identityID = User.Claims.GetIdentityID();

            await _userService.Update(userDTO, identityID);
        }
    }
}
