using Domain.Enums;
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

        public static Season? GetSeason(this DateTime date)
        {
            switch (date.Month)
            {
                case int n when n == 12 || n <= 2:
                    return Season.Winter;
                case int n when n >= 3 && n <= 5:
                    return Season.Spring;
                case int n when n >= 6 && n <= 8:
                    return Season.Summer;
                case int n when n >= 9 && n <= 11:
                    return Season.Fall;
            }

            return null;
        }
    }
}
