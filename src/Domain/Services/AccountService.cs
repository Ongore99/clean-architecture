using Domain.Common.Exceptions.DomainExceptions;
using Domain.Common.Extensions;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Accounts;
using Domain.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Domain.Services;

public class AccountService : IAccountService
{
    private readonly IValidator<Account> _accountValidator;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AccountService(IValidator<Account> accountValidator,
        IStringLocalizer<SharedResource> _localizer)
    {
        _accountValidator = accountValidator;
        this._localizer = _localizer;
    }

    public async Task Withdraw(Account account, decimal balance)
    {
        account.Balance -= balance;
        await _accountValidator.ValidateAndThrowExAsync(account, 
            x => x.Balance);
    }

    public void Transfer(Account account, Account receiverAccount, decimal amount)
    {
        account.Balance -= amount;
        receiverAccount.Balance += amount;

        if (amount < 10000)
        {
            throw new TransferAccountLimitExceededException(amount, _localizer);
        }
    }
}