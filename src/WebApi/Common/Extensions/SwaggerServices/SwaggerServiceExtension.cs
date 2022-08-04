using Domain.Common.Constants;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebApi.Common.Extensions.SwaggerServices;

public static class SwaggerServiceExtension
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }
    
    internal static void UseSwaggerUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppConstants.AppName} API v1");
            x.RoutePrefix = string.Empty;
            x.DefaultModelExpandDepth(3);
            x.DefaultModelRendering(ModelRendering.Example);
            x.DefaultModelsExpandDepth(-1);
            x.DisplayOperationId();
            x.DisplayRequestDuration();
            x.DocExpansion(DocExpansion.None);
            x.EnableDeepLinking();
            x.EnableFilter();
            x.ShowExtensions();
        });
    }
}