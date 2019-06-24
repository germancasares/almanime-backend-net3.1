﻿using Domain.Enums;
using Domain.Enums.Anime;
using System;

namespace Domain.DTOs
{
    public class AnimeDTO
    {
        public int KitsuID { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public ESeason Season { get; set; }
        public EStatus Status { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? Episodes { get; set; }
        public string Synopsis { get; set; }
        public string CoverImageUrl { get; set; }
        public string PosterImageUrl { get; set; }
    }
}
