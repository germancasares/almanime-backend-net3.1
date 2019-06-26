using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public string NickName { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
