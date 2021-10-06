using System;

namespace Template.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string ToName<T>(this T type)
        {
            return Enum.GetName(typeof(T), type);
        }

        public static string ToLowerCaseName<T>(this T type)
        {
            return Enum.GetName(typeof(T), type).ToLower();
        }

        public static string ToCammelCaseName<T>(this T type)
        {
            var name = Enum.GetName(typeof(T), type);
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
