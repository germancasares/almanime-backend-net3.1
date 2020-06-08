using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Helpers
{
    public static class ExtensionHelper
    {
        public static Guid GetIdentityID(this ClaimsPrincipal principal)
        {
            var id = principal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
        }

        public static long MbToBytes(this int mb) => mb * 1024 * 1024;

        public static string GetExtension(this IFormFile file) => Path.GetExtension(file.FileName);

        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);

        public static string GetPath(this HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}";

        public static void Emit<T>(this ILogger<T> logger, ELoggingEvent loggingEvent, string message = null,
            params object[] args)
        {
            var level = Log.LevelMap.ContainsKey(loggingEvent) ? Log.LevelMap[loggingEvent] : LogLevel.None;
            var eventId = (int)loggingEvent;
            var eventName = loggingEvent.ToString();
            var eventScope = loggingEvent.GetType().Name;
            var parameters = args.Append(eventScope).Append(eventName).ToArray();

            logger.Log(level, eventId, message + " (Event scope: {eventScope}, name: {eventName})", parameters);
        }
    }
}
