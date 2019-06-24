using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User : BaseModel
    {
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public Guid IdentityID { get; set; }

        public virtual List<Membership> Memberships { get; set; }
    }
}
