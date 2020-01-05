using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User : BaseModel
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public Guid IdentityID { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<SubtitlePartial> SubtitlePartials { get; set; }
    }
}
