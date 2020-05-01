using Newtonsoft.Json;

namespace Jobs.Models
{
    public class EpisodeDataModel
    {
        [JsonProperty("id")]
        public int Id { get; private set; }
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("attributes")]
        public EpisodeAttributesModel Attributes { get; private set; }
    }
}
