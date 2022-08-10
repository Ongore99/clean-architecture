using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.Extensions;

public static class ValidatorExtensions
{
    public static ActionResult ToBadRequest(this ValidationResult validationResult)
    {
        var validationProblem = new ValidationProblemDetails(validationResult.ToDictionary());
        return new BadRequestObjectResult(validationProblem);
    }
}