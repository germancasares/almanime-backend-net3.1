using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class Links
    {
        [JsonPropertyName("first")]
        public string First { get; set; }
        [JsonPropertyName("next")]
        public string Next { get; set; }
        [JsonPropertyName("last")]
        public string Last { get; set; }
    }
}
