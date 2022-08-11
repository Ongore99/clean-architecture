using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Accounts;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> _localizer) : base(repositoryContext, _localizer)
    {
    }

    public async Task<Account> GetUserAccount(int userId, int accountId) => 
        await FirstAsync(x => x.Id == accountId && x.UserId == userId);
}