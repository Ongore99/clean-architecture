using Domain.Entities.Transactions;

namespace Domain.Common.Contracts;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    Task<IQueryable<Transaction>> GetAccountTransactions(int accountId);
}