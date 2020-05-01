using Newtonsoft.Json;

namespace Jobs.Models
{
    public class Links
    {
        [JsonProperty("first")]
        public string First { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
