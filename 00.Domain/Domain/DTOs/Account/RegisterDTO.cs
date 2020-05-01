using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Account
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
