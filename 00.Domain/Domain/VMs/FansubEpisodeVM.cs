using System;

namespace Domain.VMs
{
    public class FansubEpisodeVM
    {
        public string AnimeSlug { get; set; }
        public string AnimeName { get; set; }
        public string AnimeCoverImage { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
