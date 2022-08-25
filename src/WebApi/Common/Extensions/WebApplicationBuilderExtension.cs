using Serilog;
using Serilog.Core;
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
    internal static void ConfigureServices(this WebApplicationBuilder builder, Logger logger)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;
        
        services.AddFluentValidators();
        services.AddSwagger();
        services.AddEndpointsApiExplorer();
        services.AddODataService();
        services.AddLocalizationService();
        services.AddErrorHandlingService(configuration, env, logger);
        services.AddMediatr();
        services.AddAppDbContext(configuration);
        services.AddRepositories();
        services.RegisterDomainServices(configuration);
    }

    internal static async Task ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        app.UseErrorHandling();
        app.UseSwaggerUi();
        app.UsePathBase(new PathString("/api"));
        app.UseRouting();
        
        app.UseLocalization();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        
        app.UseAuthorization();
        app.MapControllers();

        app.AutoMigrateDb();
        await app.Seed();
        await app.RunAsync();
    }

    internal static Logger RegisterSerilog(this WebApplicationBuilder builder)
    {
        var _logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        Log.Logger = _logger;
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(_logger);
        builder.Host.UseSerilog();

        return _logger;
    }
}