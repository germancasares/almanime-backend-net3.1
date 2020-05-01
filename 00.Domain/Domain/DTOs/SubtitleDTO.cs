using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class SubtitleDTO
    {
        public string FansubAcronym { get; set; }
        public string AnimeSlug { get; set; }
        public int EpisodeNumber { get; set; }

        public IFormFile Subtitle { get; set; }
        public ESubtitleStatus Status { get; set; }
    }
}
