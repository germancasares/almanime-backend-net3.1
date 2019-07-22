using System;
using Application.Interfaces;
using Domain.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Presentation.ActionFilters;

namespace Presentation.API.V1
{
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService
        ) : base()
        {
            _accountService = accountService;
        }

        [HttpHead("username/{username}")]
        public async Task<IActionResult> CheckUserNameAvailability(string username)
        {
            if (await _accountService.ExistsUsername(username)) return Ok();
            return NotFound();
        }

        [HttpHead("email/{email}")]
        public async Task<IActionResult> CheckEmailAvailability(string email)
        {
            if (await _accountService.ExistsEmail(email)) return Ok();
            return NotFound();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            var (token, errors) = await _accountService.CreateAccount(registerDTO);

            if (errors.Count() != 0) return BadRequest(errors);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var token = await _accountService.Login(loginDTO);

            if (token == null) return Unauthorized();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
