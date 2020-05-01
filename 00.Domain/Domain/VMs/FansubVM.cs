using System;

namespace Domain.VMs
{
    public class FansubVM
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Acronym { get; set; }
        public string FullName { get; set; }
        public string LogoUrl { get; set; }
    }
}
