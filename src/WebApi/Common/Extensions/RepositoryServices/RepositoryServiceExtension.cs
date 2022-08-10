using Domain.Common.Contracts;
using Infrastructure.Persistence.Repositories.Base;

namespace WebApi.Common.Extensions.RepositoryServices;

public static class RepositoryServiceExtension
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}