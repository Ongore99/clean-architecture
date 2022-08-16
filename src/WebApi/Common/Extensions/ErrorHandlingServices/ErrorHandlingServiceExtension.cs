using System.Net;
using System.Security.Authentication;
using Domain.Common.Exceptions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;

namespace WebApi.Common.Extensions.ErrorHandlingServices;

public static class ErrorHandlingServiceExtension
{
    internal static void AddErrorHandlingService(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddProblemDetails(opt =>
        {
            opt.IncludeExceptionDetails = (con,action) => env.IsDevelopment();

            opt.MapToStatusCode<NotFoundException>((int) HttpStatusCode.NotFound);
            opt.MapToStatusCode<ValidationException>((int) HttpStatusCode.BadRequest);
            opt.MapToStatusCode<AuthenticationCustomException>((int) HttpStatusCode.Unauthorized);
            opt.MapToStatusCode<AuthorizationException>((int) HttpStatusCode.Forbidden);
            opt.MapToStatusCode<Exception>((int) HttpStatusCode.InternalServerError);
        });
    }
}