using Domain.Common.Contracts;
using Domain.Entities.Accounts;

namespace Domain.Services;

public class AccountService
{
    private readonly IRepositoryBase<Account> _repositoryBase;

    public AccountService(IRepositoryBase<Account> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<bool> CheckAccountOwner(int accountId, int userId)
    {
        var account = await _repositoryBase.ByIdAsync(accountId);

        return account.UserId == userId;
    }
}