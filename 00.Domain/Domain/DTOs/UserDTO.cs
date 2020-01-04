using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
