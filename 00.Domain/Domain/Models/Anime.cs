using Domain.Enums;
using Domain.Enums.Anime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Anime : BaseModel
    {
        [Required]
        public int KitsuID { get; set; }

        [Required]
        public string Slug { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ESeason Season { get; set; }
        [Required]
        public EAnimeStatus Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string Synopsis { get; set; }
        public string CoverImageUrl { get; set; }
        public string PosterImageUrl { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
