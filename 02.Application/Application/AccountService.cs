using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Account;
using Domain.Enums;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenOptions = Domain.Configurations.TokenOptions;

namespace Application
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly TokenOptions _tokenOptions;
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(
            IMapper mapper,
            IUserService userService,
            IOptions<TokenOptions> tokenOptions,
            ILogger<AccountService> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _mapper = mapper;
            _userService = userService;
            _tokenOptions = tokenOptions.Value;
            _logger = logger;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> ExistsUsername(string username) => await _userManager.FindByNameAsync(username) != null;

        public async Task<bool> ExistsEmail(string email) => await _userManager.FindByEmailAsync(email) != null;

        public async Task<(JwtSecurityToken token, IEnumerable<IdentityError> errors)> CreateAccount(RegisterDTO registerDTO)
        {
            var identityUser = _mapper.Map<IdentityUser>(registerDTO);

            var result = await _userManager.CreateAsync(identityUser, registerDTO.Password);

            if (!result.Succeeded)
            {
                _logger.Emit(ELoggingEvent.CantCreateAccount, new { result.Errors });
                return (null, result.Errors);
            }

            await _userService.Create(new UserDTO { Avatar = registerDTO.Avatar, Name = identityUser.UserName }, new Guid(identityUser.Id));

            await _signInManager.SignInAsync(identityUser, false);

            return (GenerateJwtToken(identityUser), new List<IdentityError>());
        }

        public async Task<JwtSecurityToken> Login(LoginDTO loginDTO)
        {
            // The user is identified either by Email or by Username
            var user = await _userManager.FindByEmailAsync(loginDTO.Identifier) ?? await _userManager.FindByNameAsync(loginDTO.Identifier);

            if (user == null) return null;

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

            if (!signInResult.Succeeded) return null;

            return GenerateJwtToken(user);
        }

        private JwtSecurityToken GenerateJwtToken(IdentityUser identity)
        {
            var claims = new List<Claim>
            {
                new Claim("username", identity.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, identity.Id),
                new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                // Jwt ID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_tokenOptions.AccessExpirationDays));
            var notBefore = DateTime.Now;

            return new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                claims,
                notBefore,
                expires,
                creds
            );
        }
    }
}
