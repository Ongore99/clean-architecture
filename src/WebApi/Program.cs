using Serilog;
using Serilog.Core;
using WebApi.Common.Extensions;

namespace WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Starting up, time = {DateTime.UtcNow:s}");
            var builder = WebApplication.CreateBuilder(args);
            builder.RegisterSerilog();
            builder.ConfigureServices();
            builder.ConfigureApp();
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

            Log.Logger.Information("Shutting down, time = {DateTime.UtcNow:s}");
        }
    }
}