using Domain.Enums;
using System;

namespace Domain.Models
{
    public class Membership
    {
        public Guid FansubID { get; set; }
        public virtual Fansub Fansub { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public EFansubRole Role { get; set; }
    }
}
