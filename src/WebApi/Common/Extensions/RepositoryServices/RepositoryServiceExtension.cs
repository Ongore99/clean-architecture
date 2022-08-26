using Domain.Common.Contracts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Base;

namespace WebApi.Common.Extensions.RepositoryServices;

public static class RepositoryServiceExtension
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
    }
}