using Newtonsoft.Json;

namespace Jobs.Models
{
    public class Meta
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
