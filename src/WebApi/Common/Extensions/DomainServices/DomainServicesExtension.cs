namespace WebApi.Common.Extensions.DomainServices;

using Domain.Services;
using Domain.Services.Interfaces;

internal static class DomainServicesExtension
{
    public static void RegisterDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountService, AccountService>();
    }
}