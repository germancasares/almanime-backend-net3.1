using System;

namespace Domain.VMs
{
    public class EpisodeWithSubtitleVM
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime? Aired { get; set; }
        public int? Duration { get; set; }

        public SubtitleVM Subtitle { get; set; }
    }
}
