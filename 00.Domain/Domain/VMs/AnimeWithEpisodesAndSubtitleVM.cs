using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.VMs
{
    public class AnimeWithEpisodesAndSubtitleVM 
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public ESeason Season { get; set; }
        public DateTime StartDate { get; set; }

        public int EpisodesCount { get; set; }
        public ICollection<EpisodeWithSubtitleVM> Episodes { get; set; }
    }
}
