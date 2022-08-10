using FluentValidation;
using WebApi.Features.Accounts.Dtos.Requests;

namespace WebApi.Common.Extensions.FluentValidationServices;

public static class FluentValidationServiceExtension
{
    internal static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<WithdrawRequestValidator>();
    }
}