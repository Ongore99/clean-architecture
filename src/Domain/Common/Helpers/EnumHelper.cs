using Domain.Common.Constants;
using Domain.Common.Exceptions;

namespace Domain.Common.Helpers;

public static class ExceptionHelpers
{
    public static void ThrowIfNotEnum<TEnum>()
        where TEnum : struct
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new InnerException(InnerExTexts.NotEnum.Value, InnerExTexts.NotEnum.Key);
        }
    }
}