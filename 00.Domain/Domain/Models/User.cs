using System;

namespace Domain.Models
{
    public class User : BaseModel
    {
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public Guid IdentityID { get; set; }
    }
}
