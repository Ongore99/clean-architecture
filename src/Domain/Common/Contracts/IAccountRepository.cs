using Domain.Entities.Accounts;

namespace Domain.Common.Contracts;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account> GetUserAccount(int userId, int accountId);
}