using System.Collections.Generic;

namespace Domain.Models
{
    public class Fansub : BaseModel
    {
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string LogoUrl { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
