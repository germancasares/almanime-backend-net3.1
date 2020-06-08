using Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.Helpers
{
    public static class Log
    {
        public static Dictionary<ELoggingEvent, LogLevel> LevelMap { get; } = new Dictionary<ELoggingEvent, LogLevel>
        {
            { ELoggingEvent.GetItem, LogLevel.Information },
        };
    }
}
