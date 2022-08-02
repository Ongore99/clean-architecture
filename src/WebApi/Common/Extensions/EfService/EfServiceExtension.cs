using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Common.Extensions.EfService;

public static class EfServiceExtension
{
    internal static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("Default")));
    }
}