using Domain.Enums;
using Domain.Enums.Anime;
using System;
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
        public EStatus Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? Episodes { get; set; }
        public string Synopsis { get; set; }
        public string CoverImageUrl { get; set; }
        public string PosterImageUrl { get; set; }
    }
}
