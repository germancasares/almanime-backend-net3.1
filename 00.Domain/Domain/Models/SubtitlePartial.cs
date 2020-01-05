using System;

namespace Domain.Models
{
    public class SubtitlePartial
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }

        public Guid SubtitleID { get; set; }
        public virtual Subtitle Subtitle { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }
    }
}
