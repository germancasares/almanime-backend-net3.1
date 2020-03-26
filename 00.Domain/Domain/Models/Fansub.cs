using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Fansub : BaseModel
    {
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string LogoUrl { get; set; }
        public string Webpage { get; set; }
        public EFansubMainLanguage MainLanguage { get; set; }
        public EFansubMembershipOption MembershipOption { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
