using Domain.Entities.Accounts;
using FluentValidation;

namespace Domain.Common.Validations;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Balance)
            .LessThanOrEqualTo(45000)
            .When(x => x.AccountTypeId == 1);
        RuleFor(x => x.Balance)
            .LessThanOrEqualTo(60000)
            .When(x => x.AccountTypeId == 2);
    }
}