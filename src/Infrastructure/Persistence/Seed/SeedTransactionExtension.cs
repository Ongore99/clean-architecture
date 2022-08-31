using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Infrastructure.Persistence.Seed;

public static class SeedTransactionExtension
{
    public static async Task SeedTransaction(this AppDbContext dbContext)
    {
        var transactions = new List<Transaction>()
        {
            new()
            {
                Amount = -100.0m,
                AccountId = 1,
                TransactionStatusId = 1,
                TransactionTypeId = 1
            },
            new()
            {
                Amount = 10.0m,
                AccountId = 1,
                TransactionStatusId = 1,
                TransactionTypeId = 1
            },
            new()
            {
                Amount = 1.0m,
                AccountId = 1,
                TransactionStatusId = 1,
                TransactionTypeId = 1
            }
        };

        foreach (var transaction in transactions)
        {
            var entity = dbContext.Transactions
                .FirstOrDefault(y => y.AccountId == transaction.AccountId);
            if (entity is null)
            {
                await dbContext.Transactions.AddAsync(transaction);
            }
        }
    }
    
}