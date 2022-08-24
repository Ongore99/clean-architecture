using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Domain.Common.Interfaces;

namespace Domain.Common.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription<TEnum>(this TEnum item)
        => item.GetType()
            .GetField(item.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description ?? string.Empty;

    public static string GetDisplayName<TEnum>(this TEnum enumValue)
    {
        string displayName;
        displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();
        
        if (string.IsNullOrEmpty(displayName))
        {
            displayName = enumValue.ToString();
        }
        
        return displayName;
    }
}