using System.Diagnostics.CodeAnalysis;

namespace Protenacity.Examples.Extensions;

public static class Enum14Extensions
{
    extension<T>(T source) where T : struct, Enum
    {
        public static T Parse(string value)
            => Enum.Parse<T>(value);

        public static T Parse(string value, bool ignoreCase)
            => Enum.Parse<T>(value, ignoreCase);

        public static T Parse(ReadOnlySpan<char> value)
            => Enum.Parse<T>(value);

        public static T Parse(ReadOnlySpan<char> value, bool ignoreCase)
            => Enum.Parse<T>(value, ignoreCase);

        public static bool TryParse([NotNullWhen(true)] string? value, out T result)
            => Enum.TryParse(value, out result);

        public static bool TryParse([NotNullWhen(true)] string? value, bool ignoreCase, out T result)
            => Enum.TryParse(value, ignoreCase, out result);

        public static bool TryParse(ReadOnlySpan<char> value, out T result)
            => Enum.TryParse(value, out result);

        public static bool TryParse(ReadOnlySpan<char> value, bool ignoreCase, out T result)
            => Enum.TryParse(value, ignoreCase, out result);

        public static T? ParseByDescription(string? description)
            => EnumExtensions.ParseByDescription<T>(description, true, default(T));

        public static T? ParseByDescription(string? description, bool ignoreCase)
            => EnumExtensions.ParseByDescription<T>(description, ignoreCase, default(T));

        public static T? ParseByDescription(string? description, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(description, true, defaultValue);

        public static T? ParseByDescription(string? description, bool ignoreCase, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(description, ignoreCase, defaultValue);

        public static T? ParseByDescription(IEnumerable<string>? descriptions)
            => EnumExtensions.ParseByDescription<T>(descriptions, true, default(T));

        public static T? ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase)
            => EnumExtensions.ParseByDescription<T>(descriptions, ignoreCase, default(T));

        public static T? ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(descriptions, ignoreCase, defaultValue);

        public static T? ParseByDescription(IEnumerable<string>? descriptions, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(descriptions, true, defaultValue);

        public string? Description => EnumExtensions.Description<T>(source);
    }
}
