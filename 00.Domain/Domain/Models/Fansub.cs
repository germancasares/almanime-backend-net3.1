using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Fansub : BaseModel
    {
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string LogoUrl { get; set; }

        public virtual List<Membership> Memberships { get; set; }

        public Fansub()
        {
            Memberships = new List<Membership>();
        }
    }
}
