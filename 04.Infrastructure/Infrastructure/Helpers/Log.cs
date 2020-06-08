using Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public static class Log
    {
        public static Dictionary<ELoggingEvent, LogLevel> LevelMap { get; } = new Dictionary<ELoggingEvent, LogLevel>
        {
            // Services
            { ELoggingEvent.CantCreateAccount, LogLevel.Error },
            { ELoggingEvent.CantUploadSubtitle, LogLevel.Error },
            { ELoggingEvent.CantUploadSubtitlePartial, LogLevel.Error },
            { ELoggingEvent.CantLinkSubtitleUrl, LogLevel.Error },
            { ELoggingEvent.CantUploadAvatar, LogLevel.Error },

            { ELoggingEvent.UserIsNotFounder, LogLevel.Warning },
            { ELoggingEvent.UserDoesntBelongOnFansub, LogLevel.Warning },

            { ELoggingEvent.AnimeSlugDoesntExist, LogLevel.Information },
            { ELoggingEvent.UserDoesntExist, LogLevel.Information },
            { ELoggingEvent.BookmarkAlreadyExists, LogLevel.Information },
            { ELoggingEvent.BookmarkDoesntExist, LogLevel.Information },
            { ELoggingEvent.FansubDoesNotExist, LogLevel.Information },
            { ELoggingEvent.EpisodeDoesntExist, LogLevel.Information },
            { ELoggingEvent.UserAlreadyExists, LogLevel.Information },

            // Jobs
            { ELoggingEvent.AnimeStatusNotInRange, LogLevel.Warning },
            { ELoggingEvent.SlugIsDelete, LogLevel.Warning },
            { ELoggingEvent.StartDateNotRecognized, LogLevel.Warning },

            { ELoggingEvent.AnimeCreated, LogLevel.Information },
            { ELoggingEvent.AnimeUpdated, LogLevel.Information },
            { ELoggingEvent.EpisodeCreated, LogLevel.Information },
            { ELoggingEvent.EpisodeUpdated, LogLevel.Information },
        };
    }
}
