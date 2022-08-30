using System.Transactions;

namespace Domain.Common.Contracts;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    
    IBaseRepository<Transaction> TransactionRepository { get; }
    void Save();
    Task SaveAsync();
    Task BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
}