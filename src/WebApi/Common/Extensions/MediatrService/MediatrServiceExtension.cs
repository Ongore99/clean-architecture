using System.Reflection;
using MediatR;

namespace WebApi.Common.Extensions.MediatrService;

public static class MediatrServiceExtension
{
    internal static void AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}