using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class SubtitlePartialDTO
    {
        public string FansubAcronym { get; set; }
        public string AnimeSlug { get; set; }
        public int EpisodeNumber { get; set; }
        public IFormFile Partial { get; set; }
    }
}
