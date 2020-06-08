using Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public static class Log
    {
        public static Dictionary<ELoggingEvent, LogLevel> LevelMap { get; } = new Dictionary<ELoggingEvent, LogLevel>
        {
            { ELoggingEvent.AnimeCreated, LogLevel.Information },
            { ELoggingEvent.AnimeUpdated, LogLevel.Information },
            { ELoggingEvent.EpisodeCreated, LogLevel.Information },
            { ELoggingEvent.EpisodeUpdated, LogLevel.Information },

            { ELoggingEvent.AnimeStatusNotInRange, LogLevel.Warning },
            { ELoggingEvent.SlugIsDelete, LogLevel.Warning },
            { ELoggingEvent.StartDateNotRecognized, LogLevel.Warning },
        };
    }
}
