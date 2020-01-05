using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Subtitle : BaseModel
    {
        // Restriction to only allow one subtitle per Fansub.
        public ESubtitleStatus Status { get; set; }

        public Guid EpisodeID { get; set; }
        public virtual Episode Episode { get; set; }

        public Guid FansubID { get; set; }
        public virtual Fansub Fansub { get; set; }

        public string Url { get; set; }
        public ESubtitleFormat Format { get; set; }
        public virtual ICollection<SubtitlePartial> SubtitlePartials { get; set; }
    }
}
