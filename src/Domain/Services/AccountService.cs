using Domain.Common.Extensions;
using Domain.Entities.Accounts;
using Domain.Services.Interfaces;
using FluentValidation;

namespace Domain.Services;

public class AccountService : IAccountService
{
    private readonly IValidator<Account> _accountValidator;

    public AccountService(IValidator<Account> accountValidator)
    {
        _accountValidator = accountValidator;
    }

    public async Task Withdraw(Account account, decimal balance)
    {
        account.Balance -= balance;
        await _accountValidator.ValidateAndThrowExAsync(account, 
            x => x.Balance);
    }

}