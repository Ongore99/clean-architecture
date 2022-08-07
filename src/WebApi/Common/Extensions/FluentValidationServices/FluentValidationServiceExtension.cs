using System.Reflection;
using FluentValidation;

namespace WebApi.Common.Extensions.FluentValidationServices;

public static class FluentValidationServiceExtension
{
    internal static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}