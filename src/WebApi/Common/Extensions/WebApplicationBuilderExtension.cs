﻿using CustomExceptionMiddleware;
using Serilog;
using Serilog.Events;
using WebApi.Common.Extensions.EfServices;
using WebApi.Common.Extensions.MediatrServices;
using WebApi.Common.Extensions.ODataServices;
using WebApi.Common.Extensions.SwaggerServices;

namespace WebApi.Common.Extensions;

public static class WebApplicationBuilderExtension
{
    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddODataService();
        services.AddAppDbContext(configuration);
        services.AddEndpointsApiExplorer();
        services.AddMediatr();
        services.AddSwagger();
    }

    internal static void ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        app.UseSwaggerUi();

        app.UseHttpsRedirection();
        if (builder.Environment.IsDevelopment())
        {
            app.UseCustomExceptionMiddleware(
                new CustomExceptionOptions
                {

                    ViewStackTrace = true
                });
        }
        else
        {
            app.UseCustomExceptionMiddleware();
        }

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