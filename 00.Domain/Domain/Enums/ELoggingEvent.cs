namespace Domain.Enums
{
    public enum ELoggingEvent
    {
        GetItem = 1000,

        // Jobs
        SlugIsDelete = 9001,
        EAnimeStatusNotInRange = 9002,
        StartDateNotRecognized = 9003,
        AnimeUpdated = 9004,
        AnimeCreated = 9005,
        EpisodeCreated = 9006,
        EpisodeUpdated = 9007,
    }
}
