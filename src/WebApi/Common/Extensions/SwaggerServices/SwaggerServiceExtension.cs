using Domain.Common.Constants;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

namespace WebApi.Common.Extensions.SwaggerServices;

public static class SwaggerServiceExtension
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblyOf<WithdrawExamples>();
        services.TryAddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
        services.AddFluentValidationRulesToSwagger();
        
        services.AddSwaggerGen(x =>
        {
            x.DescribeAllParametersInCamelCase();
            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{nameof(WebApi)}.xml"));
            x.ExampleFilters(); 
            x.OperationFilter<LanguageHeaderFilter>();
        });
        
        services.ConfigureOptions<ConfigureSwaggerOptions>();

    }
    
    internal static void UseSwaggerUi(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider =  app.ApplicationServices
            .GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
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