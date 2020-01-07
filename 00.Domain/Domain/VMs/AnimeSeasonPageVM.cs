using System.Collections.Generic;

namespace Domain.VMs
{
    public class AnimeSeasonPageVM
    {
        public PaginationMetaVM Meta { get; set; }
        public List<AnimeVM> Animes { get; set; }
    }
}
