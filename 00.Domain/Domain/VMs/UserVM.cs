using System;

namespace Domain.VMs
{
    public class UserVM
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public Guid IdentityID { get; set; }
        public string[] Bookmarks { get; set; }
    }
}
