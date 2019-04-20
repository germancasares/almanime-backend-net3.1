using Newtonsoft.Json;

namespace Functions.Models
{
    public class Meta
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
