using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.API.V1
{
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IMapper mapper,
            IUserService userService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }
    }
}
