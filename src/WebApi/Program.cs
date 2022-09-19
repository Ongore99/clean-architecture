using Serilog;
using Serilog.Core;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.SerialogServices;

namespace WebApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Starting up, time = {DateTime.UtcNow:s}");
            var builder = WebApplication.CreateBuilder(args);
            var logger = builder.RegisterSerilog();
            Log.Logger.Information($"Starting up, time = {DateTime.UtcNow:s}");
            builder.ConfigureServices(logger);
            await builder.ConfigureApp();
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Application start-up failed. Exception = '{ex.Message}'");
            Log.Logger.Error(ex, "Application start-up failed.");
        }
        finally
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Shutting down, time = {DateTime.UtcNow:s}");
            Console.ResetColor();

            Log.Logger.Information($"Shutting down, time = {DateTime.UtcNow:s}");
        }
    }
}