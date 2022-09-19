using Gridify;
using Serilog;
using Serilog.Core;
using WebApi.Common.Extensions.DomainServices;
using WebApi.Common.Extensions.EfServices;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Common.Extensions.FluentValidationServices;
using WebApi.Common.Extensions.GridifyServices;
using WebApi.Common.Extensions.LocalizationServices;
using WebApi.Common.Extensions.MapsterServices;
using WebApi.Common.Extensions.MediatrServices;
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
        
        services.AddMapster();
        services.AddFluentValidators();
        services.AddSwagger();
        services.AddGridify(configuration);
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddLocalizationService();
        services.AddErrorHandlingService(configuration, env, logger);
        services.AddMediatr();
        services.AddAppDbContext(configuration, env);
        services.AddRepositories();
        services.RegisterDomainServices(configuration);
    }

    internal static async Task ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        app.UseErrorHandling();
        app.UseSwaggerUi();
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

}