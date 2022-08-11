using Domain.Entities.Accounts;

namespace Domain.Services.Interfaces;

public interface IAccountService
{
    Task Withdraw(Account account, decimal balance);
}