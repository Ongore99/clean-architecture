using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Common.Extensions.EfService;

public static class EfAppBuilderExtension
{
    internal static void AutoMigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }
}