using Microsoft.AspNetCore.OData;
using Serilog;
using Serilog.Events;
using WebApi.Common.Extensions.EfService;
using WebApi.Common.Extensions.MediatrService;
using WebApi.Common.Extensions.ODataService;

namespace WebApi.Common.Extensions;

public static class WebApplicationBuilderExtension
{
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
    
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddODataService();
        services.AddAppDbContext(configuration);
        services.AddEndpointsApiExplorer();
        services.AddMediatr();
        services.AddSwaggerGen();
    }
    
    internal static void ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.AutoMigrateDb();
        app.Run();
    }
}