using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jobs.Models
{
    public class EpisodeCollection
    {
        [JsonPropertyName("data")]
        public List<EpisodeDataModel> Data { get; private set; }
        [JsonPropertyName("meta")]
        public Meta Meta { get; private set; }
        [JsonPropertyName("links")]
        public Links Links { get; private set; }
    }
}
