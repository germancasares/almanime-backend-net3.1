namespace Jobs.UpdateEpisodeTable.Contracts
{
    public class CAnime
    {
        public const string QUEUE_NAME = "update-episode-table";

        public string ID { get; set; }
        public string Slug { get; set; }
    }
}
