using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.FluentValidationServices;

namespace WebApi.Common.Extensions;

public static class ValidatorExtensions
{
    public static ActionResult ToBadRequest(this ValidationResult validationResult)
    {
        var validationProblem = new CustomValidationProblem();

        foreach (var failure in validationResult.Errors)
        {
            int code; 
            int.TryParse(failure.ErrorCode, out code);
            
            bool success = validationProblem.Errors.TryAdd(failure.PropertyName,  new List<CustomFailure>
            {
                new CustomFailure
                { 
                    ErrorCode = code == default ? null : code,
                    ErrorMessage = failure.ErrorMessage
                }
            });
            
            if (!success)
            {
                validationProblem.Errors[failure.PropertyName].Add(
                    new CustomFailure
                    { 
                        ErrorCode = code == default ? null : code,
                        ErrorMessage = failure.ErrorMessage
                    });
            }
        }
        
        return new BadRequestObjectResult(validationProblem);
    }
}
