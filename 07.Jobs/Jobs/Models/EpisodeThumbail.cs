using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class EpisodeThumbail
    {
        [JsonPropertyName("original")]
        public string Original { get; private set; }
        [JsonPropertyName("tiny")]
        public string Tiny { get; private set; }
        [JsonPropertyName("small")]
        public string Small { get; private set; }
        [JsonPropertyName("large")]
        public string Large { get; private set; }
    }
}
