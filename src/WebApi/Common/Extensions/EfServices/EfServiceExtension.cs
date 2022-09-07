using Infrastructure.Persistence;
using Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.EfServices;

public static class EfServiceExtension
{
    internal static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();
        services.AddDbContext<AppDbContext>(
            (serviceProvider, options) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!
                    .Value;
                
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });
                
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });
    }
    
    internal static void AutoMigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        context.Database.Migrate();
    }
    
    internal static async Task Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.SeedData();
    }
    
}