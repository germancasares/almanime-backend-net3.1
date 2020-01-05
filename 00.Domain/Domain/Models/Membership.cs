using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Membership : BaseModel
    {
        public Guid FansubID { get; set; }
        public virtual Fansub Fansub { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public EFansubRole Role { get; set; }
    }
}
