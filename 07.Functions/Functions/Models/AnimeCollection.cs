using Kitsu.Anime;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Functions.Models
{
    public class AnimeCollection
    {
        [JsonProperty("data")]
        public List<AnimeDataModel> Data { get; private set; }
        [JsonProperty("meta")]
        public Meta Meta { get; private set; }
        [JsonProperty("links")]
        public Links Links { get; private set; }
    }
}
