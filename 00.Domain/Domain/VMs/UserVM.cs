using System;

namespace Domain.VMs
{
    public class UserVM
    {
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public Guid IdentityID { get; set; }
    }
}
