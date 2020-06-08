using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class Meta
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
