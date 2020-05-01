using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jobs.Models
{
    public class EpisodeCollection
    {
        [JsonProperty("data")]
        public List<EpisodeDataModel> Data { get; private set; }
        [JsonProperty("meta")]
        public Meta Meta { get; private set; }
        [JsonProperty("links")]
        public Links Links { get; private set; }
    }
}
