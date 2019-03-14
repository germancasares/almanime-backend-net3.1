using Newtonsoft.Json;

namespace Functions.Models
{
    public class Meta
    {
        [JsonProperty("count")]
        public string Count { get; set; }
    }
}
