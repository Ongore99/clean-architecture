using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Common.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var validationProblem = new ValidationProblemDetails(context.ModelState);
            context.Result = new BadRequestObjectResult(validationProblem);
            
            return;
        }

        await next();
    }
}