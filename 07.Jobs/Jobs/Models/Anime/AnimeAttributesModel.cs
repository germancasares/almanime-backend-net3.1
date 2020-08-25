namespace Jobs.Models.Anime
{
    public class AnimeAttributesModel
    {
        public string Slug { get; set; }
        public string CanonicalTitle { get; set; }
        public string Synopsis { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Subtype { get; set; }
        public AnimeCoverImageModel CoverImage { get; set; }
        public AnimePosterImageModel PosterImage { get; set; }
    }
}