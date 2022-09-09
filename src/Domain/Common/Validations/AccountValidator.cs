using Domain.Common.Resources;
using Domain.Entities.Accounts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Domain.Common.Validations;

public class AccountValidator : AbstractValidator<Account>
{
    private readonly IStringLocalizer _localizer;

    public AccountValidator(IStringLocalizer<AccountResource> _localizer)
    {
        this._localizer = _localizer;
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0)
            .WithErrorCode("7");
        RuleFor(x => x.Balance)
            .LessThanOrEqualTo(300000)
            .When(x => x.AccountTypeId == 1);
        RuleFor(x => x.Balance)
            .LessThanOrEqualTo(400000)
            .When(x => x.AccountTypeId == 2);
    }
}