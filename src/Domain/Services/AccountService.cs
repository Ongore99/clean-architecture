using Domain.Common.Extensions;
using Domain.Common.Resources;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Accounts;
using Domain.Entities.Accounts.Exceptions;
using Domain.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Domain.Services;

public class AccountService : IAccountService
{
    private readonly IValidator<Account> _accountValidator;
    private readonly IStringLocalizer<AccountResource> _localizer;

    public AccountService(IValidator<Account> accountValidator,
        IStringLocalizer<AccountResource> _localizer)
    {
        _accountValidator = accountValidator;
        this._localizer = _localizer;
    }

    public async Task Withdraw(Account account, decimal balance)
    {
        
        if (account.AccountTypeId == 1 && balance > 4000)
        {
            throw new WithdrawAccountLimitExceededException( (AccountTypeEnum) account.AccountTypeId, balance, _localizer);
        }
        
        if (account.AccountTypeId == 1 && balance > 10000)
        {
            throw new WithdrawAccountLimitExceededException((AccountTypeEnum) account.AccountTypeId, balance, _localizer);
        }
        
        account.Balance -= balance;
        await _accountValidator.ValidateAndThrowExAsync(account, 
            x => x.Balance);
    }

    public async Task Transfer(Account account, Account receiverAccount, decimal amount)
    {
        if (amount > 3000 && account.AccountTypeId == 1)
        {
            throw new TransferAccountLimitExceededException((AccountTypeEnum) account.AccountTypeId, amount, _localizer);
        }
        
        if (amount > 5000 && account.AccountTypeId == 2)
        {
            throw new TransferAccountLimitExceededException((AccountTypeEnum) account.AccountTypeId, amount, _localizer);
        }
        
        account.Balance -= amount;
        receiverAccount.Balance += amount;
        await _accountValidator.ValidateAndThrowExAsync(account, 
            x => x.Balance);
        await _accountValidator.ValidateAndThrowExAsync(receiverAccount, 
            x => x.Balance);
    }
}