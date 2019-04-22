using Domain.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> ExistsUsername(string username);
        Task<bool> ExistsEmail(string email);
        Task<(JwtSecurityToken token, IEnumerable<IdentityError> errors)> CreateAccount(RegisterDTO registerDTO);
        Task<JwtSecurityToken> Login(LoginDTO loginDTO);
    }
}
