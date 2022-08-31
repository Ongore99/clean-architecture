using Core.Common.Bases;
using Mapster;

namespace WebApi.Common.Extensions.MapsterServices;

public static class MapsterExtension
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        var applicationAssembly = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssembly);
    }
}