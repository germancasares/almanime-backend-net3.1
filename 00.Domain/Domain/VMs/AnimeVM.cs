using System;

namespace Domain.VMs
{
    public class AnimeVM
    {
        public Guid ID { get; set; }

        public int KitsuID { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public string Status { get; set; }
        public string Synopsis { get; set; }
        public DateTime StartDate { get; set; }

        public string CoverImage { get; set; }
        public string PosterImage { get; set; }
    }
}
