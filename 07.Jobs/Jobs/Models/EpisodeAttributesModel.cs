using Newtonsoft.Json;

namespace Jobs.Models
{
    public class EpisodeAttributesModel
    {
        [JsonProperty("canonicalTitle")]
        public string CanonicalTitle { get; private set; }

        [JsonProperty("seasonNumber")]
        public int? SeasonNumber { get; private set; }

        [JsonProperty("number")]
        public int? Number { get; private set; }

        [JsonProperty("relativeNumber")]
        public int? RelativeNumber { get; private set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; private set; }

        [JsonProperty("airdate")]
        public string Airdate { get; private set; }

        [JsonProperty("length")]
        public int? Length { get; private set; }

        [JsonProperty("thumbnail")]
        public EpisodeThumbail Thumbnail { get; private set; }
    }
}
