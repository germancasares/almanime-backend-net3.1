using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class AnimeDTO
    {
        public int KitsuID { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public ESeason Season { get; set; }
        public EAnimeStatus Status { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Synopsis { get; set; }
        public string CoverImageUrl { get; set; }
        public string PosterImageUrl { get; set; }

        public IEnumerable<EpisodeDTO> Episodes { get; set; }
    }
}
