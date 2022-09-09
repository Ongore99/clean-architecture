using Domain.Entities.Accounts;

namespace Domain.Services.Interfaces;

public interface IAccountService
{
    Task Withdraw(Account account, decimal balance);

    Task Transfer(Account account, Account receiverAccount, decimal amount);
}