using Domain.Enums;

namespace Domain.VMs
{
    public class FansubUserVM
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public EFansubRole Role { get; set; }
    }
}
