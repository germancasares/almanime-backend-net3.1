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

namespace Presentation.API.V1
{
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (token, errors) = await _accountService.CreateAccount(registerDTO);

            if (errors.Count() != 0) return BadRequest(errors);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _accountService.Login(loginDTO);

            if (token == null) return Unauthorized();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
