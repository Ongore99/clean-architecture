using Gridify;

namespace WebApi.Common.Extensions.GridifyServices;

public static class GridifyServiceExtension
{
    internal static void AddGridify(this IServiceCollection services, ConfigurationManager config)
    {
        services.ConfigureOptions<GridifyOptionsSetup>();
        var settings = config
            .GetSection(GridifyOptionsSetup.ConfigurationSectionName)
            .Get<GridifyOptions>();
        
        GridifyGlobalConfiguration.EntityFrameworkCompatibilityLayer = true;
        GridifyGlobalConfiguration.CaseSensitiveMapper = false;
        GridifyGlobalConfiguration.DefaultPageSize = settings.DefaultPageSize;
        GridifyGlobalConfiguration.AllowNullSearch = settings.AllowNullSearch;
    }
}