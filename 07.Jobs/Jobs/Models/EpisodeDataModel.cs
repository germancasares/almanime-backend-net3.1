using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class EpisodeDataModel
    {
        [JsonPropertyName("id")]
        public int Id { get; private set; }
        [JsonPropertyName("type")]
        public string Type { get; private set; }
        [JsonPropertyName("attributes")]
        public EpisodeAttributesModel Attributes { get; private set; }
    }
}
