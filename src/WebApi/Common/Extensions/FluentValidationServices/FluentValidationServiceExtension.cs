using Domain.Common.Validations;
using FluentValidation;
using WebApi.Endpoints.Accounts.Dtos.Requests;

namespace WebApi.Common.Extensions.FluentValidationServices;

public static class FluentValidationServiceExtension
{
    internal static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<WithdrawRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountValidator>();
    }
}