using System.Net;
using Domain.Common.Exceptions;
using Hellang.Middleware.ProblemDetails;

namespace WebApi.Common.Extensions.ErrorHandlingServices;

public static class ErrorHandlingServiceExtension
{
    internal static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddProblemDetails(opt =>

        {
            opt.IncludeExceptionDetails = (con,action) => env.IsDevelopment();

            opt.MapToStatusCode<NotFoundException>((int) HttpStatusCode.NotFound);
            opt.MapToStatusCode<Exception>((int) HttpStatusCode.InternalServerError);
        });
    }
}