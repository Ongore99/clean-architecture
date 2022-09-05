using Gridify;

namespace WebApi.Common.Extensions.GridifyServices;

public static class GridifyServiceExtension
{
    internal static void AddGridify(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<GridifySetting>(
            x => config.GetSection(nameof(GridifySetting))
            .Bind(x));
        var settings = config.GetSection(nameof(GridifySetting)).Get<GridifySetting>();
        
        GridifyGlobalConfiguration.EntityFrameworkCompatibilityLayer = true;
        GridifyGlobalConfiguration.CaseSensitiveMapper = false;
        GridifyGlobalConfiguration.DefaultPageSize = settings.DefaultPageSize;
        GridifyGlobalConfiguration.AllowNullSearch = settings.AllowNullSearch;
    }
}