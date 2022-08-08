using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Common.Extensions.EfServices;

public static class EfServiceExtension
{
    internal static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
            

        });
    }
    
    internal static void AutoMigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        context.Database.Migrate();
    }
    
}