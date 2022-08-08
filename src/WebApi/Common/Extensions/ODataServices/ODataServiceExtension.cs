using Microsoft.AspNetCore.OData;
using WebApi.Common.Filters;

namespace WebApi.Common.Extensions.ODataServices;

public static class ODataServiceExtension
{
    internal static void AddODataService(this IServiceCollection services)
    {
        services.AddControllers(x =>
                x.Filters.Add<ValidationFilter>()
                )
            .AddOData(options =>
            {
                options
                    .Select()
                    .Filter()
                    .OrderBy()
                    .SetMaxTop(100);
            });
    }
}