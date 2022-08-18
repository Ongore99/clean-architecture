using System.Net;
using System.Security.Authentication;
using Domain.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.Extensions.ErrorHandlingServices;

public static class ErrorHandlingServiceExtension
{
    internal static void AddErrorHandlingService(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddProblemDetails(opt =>
        {
            opt.IncludeExceptionDetails = (con,action) => env.IsDevelopment();
            opt.MapToStatusCode<NotFoundException>((int) HttpStatusCode.NotFound);
            opt.Map<ValidationException>(x => x.ToValidationProblemDetails());
            opt.MapToStatusCode<AuthenticationCustomException>((int) HttpStatusCode.Unauthorized);
            opt.MapToStatusCode<AuthorizationException>((int) HttpStatusCode.Forbidden);
            opt.MapToStatusCode<Exception>((int) HttpStatusCode.InternalServerError);
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