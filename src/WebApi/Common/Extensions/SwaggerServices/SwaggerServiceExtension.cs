using Domain.Common.Constants;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.Features.Accounts.Dtos.SwaggeExamples;

namespace WebApi.Common.Extensions.SwaggerServices;

public static class SwaggerServiceExtension
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblyOf<WithdrawExamples>();

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ezra api",
                Version = "v1",
            });
            x.DescribeAllParametersInCamelCase();
            
            x.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{nameof(WebApi)}.xml"));
        });
        
    
        services.AddFluentValidationRulesToSwagger();
    }
    
    internal static void UseSwaggerUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppConstants.AppName} API v1");
            x.RoutePrefix = "";
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