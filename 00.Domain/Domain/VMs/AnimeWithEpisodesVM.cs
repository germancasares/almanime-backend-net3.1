using System.Collections.Generic;

namespace Domain.VMs
{
    public class AnimeWithEpisodesVM: AnimeVM
    {
        public ICollection<EpisodeVM> Episodes { get; set; }
    }
}
