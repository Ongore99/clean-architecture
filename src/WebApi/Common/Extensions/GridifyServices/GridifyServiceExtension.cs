using Gridify;

namespace WebApi.Common.Extensions.GridifyServices;

public static class GridifyServiceExtension
{
    internal static void AddGridify(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<Gridify>(
            x => config.GetSection(nameof(Gridify))
            .Bind(x));
        var settings = config.GetSection(nameof(Gridify)).Get<Gridify>();
        
        GridifyGlobalConfiguration.EntityFrameworkCompatibilityLayer = true;
        GridifyGlobalConfiguration.CaseSensitiveMapper = false;
        GridifyGlobalConfiguration.DefaultPageSize = settings.DefaultPageSize;
        GridifyGlobalConfiguration.AllowNullSearch = settings.AllowNullSearch;
    }
}