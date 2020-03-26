using Domain.Enums;
using System;

namespace Domain.DTOs
{
    public class FansubDTO
    {
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string Webpage { get; set; }
        public EFansubMainLanguage MainLanguage { get; set; }
        public EFansubMembershipOption MembershipOption { get; set; }
    }
}
