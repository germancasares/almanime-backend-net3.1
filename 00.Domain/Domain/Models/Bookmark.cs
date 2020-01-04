using System;

namespace Domain.Models
{
    public class Bookmark : BaseModel
    {
        public Guid AnimeID { get; set; }
        public virtual Anime Anime { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }
    }
}
