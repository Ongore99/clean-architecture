using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebApi.Features.Accounts.Dtos.Requests;

namespace WebApi.Common.Extensions.FluentValidationServices;

public static class FluentValidationServiceExtension
{
    internal static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(x => 
        {
            x.DisableDataAnnotationsValidation = true;
        });
        
        services.AddValidatorsFromAssemblyContaining<WithdrawRequestValidator>();
    }
}