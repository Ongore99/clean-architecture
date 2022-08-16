using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Common.Extensions;

public static class ValidatorExtensions
{
    public static async Task<ValidationResult> ValidateAndThrowExAsync<T>(this IValidator<T> validator, 
        T instance, 
        params Expression<Func<T, object>>[] propertyExpressions)
    {
        var validationProblem = await validator.ValidateAsync(instance,
            opt =>
            {
                opt.IncludeProperties(propertyExpressions);
                opt.ThrowOnFailures();
            });

        return validationProblem;
    }
}