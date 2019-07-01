using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Chapter : BaseModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime? Aired { get; set; }
        public int? Duration { get; set; }

        public Guid AnimeID { get; set; }
        public virtual Anime Anime { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
