using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class EpisodeAttributesModel
    {
        [JsonPropertyName("canonicalTitle")]
        public string CanonicalTitle { get; private set; }

        [JsonPropertyName("seasonNumber")]
        public int? SeasonNumber { get; private set; }

        [JsonPropertyName("number")]
        public int? Number { get; private set; }

        [JsonPropertyName("relativeNumber")]
        public int? RelativeNumber { get; private set; }

        [JsonPropertyName("synopsis")]
        public string Synopsis { get; private set; }

        [JsonPropertyName("airdate")]
        public string Airdate { get; private set; }

        [JsonPropertyName("length")]
        public int? Length { get; private set; }

        [JsonPropertyName("thumbnail")]
        public EpisodeThumbail Thumbnail { get; private set; }
    }
}
