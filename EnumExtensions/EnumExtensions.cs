using System.ComponentModel;

namespace Protenacity.Examples.Extensions;

public static class EnumExtensions
{
    public static T? ParseByDescription<T>(string? description, bool ignoreCase, T? defaultValue)
    {
        var fields = typeof(T).GetFields();
        if (fields?.Any() != true)
        {
            throw new ApplicationException(nameof(T) + " doesn't haved any values");
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            foreach (var field in fields.Skip(1))
            {
                if ((Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                    && string.Compare(attribute.Description, description, ignoreCase) == 0) || string.Compare(field.Name, description, ignoreCase) == 0)
                {
                    return (T)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }

        return defaultValue;
    }

    public static T? ParseByDescription<T>(IEnumerable<string>? descriptions, bool ignoreCase, T? defaultValue)
    {
        var fields = typeof(T).GetFields();
        if (fields?.Any() != true)
        {
            throw new ApplicationException(nameof(T) + " doesn't haved any values");
        }

        if (descriptions?.Any() != true)
        {
            return defaultValue;
        }

        int value = 0;

        foreach (var description in descriptions)
        {
            foreach (var field in fields.Skip(1))
            {
                if ((Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                    && string.Compare(attribute.Description, description, ignoreCase) == 0) || string.Compare(field.Name, description, ignoreCase) == 0)
                {
                    value = value | (int)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }

        return value == 0 ? defaultValue : (T)(object)value;
    }

    public static string? Description<T>(T source)
        => (typeof(T).GetField(source?.ToString() ?? throw new ArgumentNullException())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute)?.Description;
}

