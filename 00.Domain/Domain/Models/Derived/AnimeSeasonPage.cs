using System.Collections.Generic;

namespace Domain.Models.Derived
{
    public class AnimeSeasonPage
    {
        public List<Anime> Animes { get; set; }
        public PaginationMeta Meta { get; set; }
    }
}
