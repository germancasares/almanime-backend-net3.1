using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Subtitle : BaseModel
    {
        //TODO: Make this property be "Derived" from one of the multiple rows from Subtitle_History table.
        //public Fansub Fansub { get; set; }

        // Restriction to only allow one subtitle per Fansub.
        public ESubtitleStatus Status { get; set; }

        public Guid ChapterID { get; set; }
        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<SubtitlePartial> SubtitlePartials { get; set; }
    }
}
