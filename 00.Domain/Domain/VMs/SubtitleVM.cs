using Domain.Enums;

namespace Domain.VMs
{
    public class SubtitleVM
    {
        // Restriction to only allow one subtitle per Fansub.
        public string Status { get; set; }
        public string Format { get; set; }
        public string Url { get; set; }
        public string FansubAcronym { get; set; }
        public string AnimeSlug { get; set; }
        public int EpisodeNumber { get; set; }
    }
}
