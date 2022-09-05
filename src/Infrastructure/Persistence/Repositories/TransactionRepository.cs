using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }

    public Task<IQueryable<Transaction>> GetAccountTransactions(int accountId)
    {
        throw new NotImplementedException();
    }
}