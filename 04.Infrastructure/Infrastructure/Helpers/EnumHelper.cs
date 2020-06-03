using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace Infrastructure.Helpers
{
    public static class EnumHelper
    {
        public static T? GetEnumFromString<T>(string value) where T : struct
        {
            var isEnum = Enum.TryParse<T>(value, out var type);

            return isEnum ? type : (T?)null;
        }

        public static ESeason GetSeason(DateTime date) => GetSeason(date.Month);

        public static ESeason GetSeason(int month)
        {
            return month switch
            {
                int n when n == 12 || n <= 2 => ESeason.Winter,
                int n when n >= 3 && n <= 5 => ESeason.Spring,
                int n when n >= 6 && n <= 8 => ESeason.Summer,
                int n when n >= 9 && n <= 11 => ESeason.Fall,
                _ => throw new ArgumentException("Month out of ESeason range."),
            };
        }

        public static ESubtitleFormat GetSubtitleFormat(this IFormFile file)
        {
            return (file.GetExtension()) switch
            {
                ".ass" => ESubtitleFormat.ASS,
                ".srt" => ESubtitleFormat.SRT,
                _ => throw new ArgumentException("Extension out of ESubtitleFormat range."),
            };
        }
    }
}
