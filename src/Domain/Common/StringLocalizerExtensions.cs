using Microsoft.Extensions.Localization;

namespace Domain.Common;

public static class StringLocalizerExtensions
{
    public static string Text<T>(this IStringLocalizer<T> stringLocalizer, string key, string arg)
        => string.Format(stringLocalizer[key].Value, arg);
}