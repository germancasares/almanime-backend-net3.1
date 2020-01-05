using System;

namespace Domain.Models
{
    public class SubtitlePartial
    {
        public Guid SubtitleID { get; set; }
        public virtual Subtitle Subtitle { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public int Revision { get; set; }
        public DateTime RevisionDate { get; set; }
    }
}
