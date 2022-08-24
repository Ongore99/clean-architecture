using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Persistence.Seed;

public static class SeedEnumExtension
{
    public static void SeedEnums(this AppDbContext dbContext)
    {
        dbContext.AccountTypes.AddOrUpdateEnumValues<AccountType, AccountTypeEnum>(
            @enum => new AccountType(@enum));
        dbContext.TransactionStatuses.AddOrUpdateEnumValues<TransactionStatus, TransactionStatusEnum>(
            @enum => new TransactionStatus(@enum));
        dbContext.AccountStatuses.AddOrUpdateEnumValues<AccountStatus, AccountStatusEnum>(
            @enum => new AccountStatus(@enum));
        dbContext.TransactionsTypes.AddOrUpdateEnumValues<TransactionType, TransactionTypeEnum>(
            @enum => new TransactionType(@enum));
    }
}