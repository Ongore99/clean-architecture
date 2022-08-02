using Microsoft.AspNetCore.OData;

namespace WebApi.Common.Extensions.ODataService;

public static class ODataServiceExtension
{
    internal static void AddODataService(this IServiceCollection services)
    {
        services.AddControllers()
            .AddOData(options => options
                .Select()
                .Filter()
                .OrderBy());
    }
}