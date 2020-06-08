namespace Domain.Enums
{
    public enum ELoggingEvent
    {
        GetItem = 1000,

        // Services
        CantCreateAccount = 2001,
        AnimeSlugDoesntExist = 2002,
        UserDoesntExist = 2003,
        BookmarkAlreadyExists = 2004,
        BookmarkDoesntExist = 2005,
        UserIsNotFounder = 2006,
        FansubDoesNotExist = 2007,
        EpisodeDoesntExist = 2008,
        UserDoesntBelongOnFansub = 2009,
        CantUploadSubtitle = 2010,
        CantUploadSubtitlePartial = 2011,
        CantLinkSubtitleUrl = 2012,
        UserAlreadyExists = 2013,
        CantUploadAvatar = 2014,

        // Jobs
        SlugIsDelete = 7001,
        AnimeStatusNotInRange = 7002,
        StartDateNotRecognized = 7003,
        AnimeUpdated = 7004,
        AnimeCreated = 7005,
        EpisodeCreated = 7006,
        EpisodeUpdated = 7007,
    }
}
