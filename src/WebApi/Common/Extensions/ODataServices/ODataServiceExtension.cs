using Microsoft.AspNetCore.OData;

namespace WebApi.Common.Extensions.ODataServices;

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