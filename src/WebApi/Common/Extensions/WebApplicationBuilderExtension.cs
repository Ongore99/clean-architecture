using Hellang.Middleware.ProblemDetails;
using Serilog;
using Serilog.Events;
using WebApi.Common.Extensions.DomainServices;
using WebApi.Common.Extensions.EfServices;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Common.Extensions.FluentValidationServices;
using WebApi.Common.Extensions.LocalizationServices;
using WebApi.Common.Extensions.MediatrServices;
using WebApi.Common.Extensions.ODataServices;
using WebApi.Common.Extensions.RepositoryServices;
using WebApi.Common.Extensions.SwaggerServices;

namespace WebApi.Common.Extensions;

public static class WebApplicationBuilderExtension
{
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;
        
        services.AddFluentValidators();
        services.AddSwagger();
        services.AddEndpointsApiExplorer();
        services.AddODataService();
        services.AddLocalizationService();
        services.AddErrorHandlingService(configuration, env);
        services.AddMediatr();
        services.AddAppDbContext(configuration);
        services.AddRepositories();
        services.RegisterDomainServices(configuration);
    }

    internal static void ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        app.UseProblemDetails();
        app.UseSwaggerUi();

        app.UseLocalization();
        app.UseHttpsRedirection();
        
        app.UseAuthorization();
        app.MapControllers();

        app.AutoMigrateDb();
        app.Run();
    }

    internal static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        Console.WriteLine($"Starting up, time = {DateTime.UtcNow:s}");
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console());
    }
}