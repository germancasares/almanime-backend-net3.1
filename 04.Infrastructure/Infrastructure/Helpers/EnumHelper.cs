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

        public static ESeason GetSeason(int month)
        {
            switch (month)
            {
                case int n when n == 12 || n <= 2:
                    return ESeason.Winter;
                case int n when n >= 3 && n <= 5:
                    return ESeason.Spring;
                case int n when n >= 6 && n <= 8:
                    return ESeason.Summer;
                case int n when n >= 9 && n <= 11:
                    return ESeason.Fall;
                default: throw new ArgumentException("Month out of valid range.");
            }
        }
    }
}
