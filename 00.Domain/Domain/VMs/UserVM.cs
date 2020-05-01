using System;
using System.Collections.Generic;

namespace Domain.VMs
{
    public class UserVM
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public Guid IdentityID { get; set; }
        public List<string> Bookmarks { get; set; }
    }
}
