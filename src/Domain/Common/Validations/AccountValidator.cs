using Domain.Entities.Accounts;
using FluentValidation;

namespace Domain.Common.Validations;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
    }
}