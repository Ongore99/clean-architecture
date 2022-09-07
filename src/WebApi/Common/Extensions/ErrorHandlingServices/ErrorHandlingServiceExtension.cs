using System.Net;
using System.Security.Authentication;
using Domain.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using WebApi.Common.Bases;

namespace WebApi.Common.Extensions.ErrorHandlingServices;

public static class ErrorHandlingServiceExtension
{
    internal static void AddErrorHandlingService(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment env, Logger logger)
    {
        services.AddProblemDetails(opt =>
        {
            opt.IncludeExceptionDetails = (con,action) => env.IsDevelopment();
            opt.ShouldLogUnhandledException = 
                (_, ex, d) => d.Status is null or >= 500;
            opt.OnBeforeWriteDetails = (ctx, pr) =>
            {
                logger.Error($"Exception occured:\n{pr.Title}\n{pr.Detail}");
                logger.Error(pr.Instance);
            };
            
            opt.MapToStatusCode<NotFoundException>((int) HttpStatusCode.NotFound);
            opt.Map<ValidationException>(x => x.ToValidationProblemDetails());
            opt.MapToStatusCode<AuthenticationCustomException>((int) HttpStatusCode.Unauthorized);
            opt.MapToStatusCode<AuthorizationException>((int) HttpStatusCode.Forbidden);
            opt.Map<DomainException>(ex => new CustomProblemDetails()
            {
                Title = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://httpstatuses.io/500",
                AdditionalData = new Dictionary<string, object?>
                {
                    { "code", ex.Code }
                }
            });
        });
    }
    
    internal static void UseErrorHandling(this IApplicationBuilder app)
    {
        app.UseProblemDetails();
    }
    
    private static ValidationProblemDetails ToValidationProblemDetails(this ValidationException e)
    {
        return new ValidationProblemDetails(
            e.Errors
            .GroupBy(y => y.PropertyName)
            .ToDictionary(y => y.Key, z =>
                z.Select(t => t.ErrorMessage)
                    .ToArray()));
    }
}