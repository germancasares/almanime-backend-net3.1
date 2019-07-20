using Newtonsoft.Json;

namespace Jobs.Models
{
    public class EpisodeThumbail
    {
        [JsonProperty("original")]
        public string Original { get; private set; }
        [JsonProperty("tiny")]
        public string Tiny { get; private set; }
        [JsonProperty("small")]
        public string Small { get; private set; }
        [JsonProperty("large")]
        public string Large { get; private set; }
    }
}
