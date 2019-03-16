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
        public Season Season { get; set; }
        [Required]
        public Status Status { get; set; }

        public string Synopsis { get; set; }
        public int? Episodes { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
