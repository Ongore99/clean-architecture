using Serilog;
using Serilog.Core;

namespace WebApi.Common.Extensions.SerialogServices;

public static class SerialogServices
{
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