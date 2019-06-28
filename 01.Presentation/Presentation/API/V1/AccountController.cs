using System;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Presentation.ActionFilters;

namespace Presentation.API.V1
{
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IMapper mapper,
            IUserService userService,
            IAccountService accountService
        ) : base()
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;

            _userService = userService;
            _accountService = accountService;
        }

        [HttpHead("Username/{username}")]
        public async Task<IActionResult> CheckUserNameAvailability(string username)
        {
            if (await _accountService.ExistsUsername(username)) return Ok();
            return NotFound();
        }

        [HttpHead("Email/{email}")]
        public async Task<IActionResult> CheckEmailAvailability(string email)
        {
            if (await _accountService.ExistsEmail(email)) return Ok();
            return NotFound();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var (token, errors) = await _accountService.CreateAccount(registerDTO);

            if (errors.Count() != 0) return BadRequest(errors);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var token = await _accountService.Login(loginDTO);

            if (token == null) return Unauthorized();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
