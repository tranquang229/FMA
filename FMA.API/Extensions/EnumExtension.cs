using System.ComponentModel;
using System.Reflection;

namespace FMA.API.Extensions;

public static class EnumExtensions
{
    public static string ToDescriptionString<TEnum>(this TEnum @enum)
    {
        FieldInfo info = @enum.GetType().GetField(@enum.ToString());
        var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes?[0].Description ?? @enum.ToString();
    }

    public static T GetValueFromDescription<T>(this string description)
    {
        var type = typeof(T);
        if (!type.IsEnum) throw new InvalidOperationException();
        foreach (var field in type.GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException("Not found.", nameof(description));
    }
}