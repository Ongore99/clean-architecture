using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Persistence.Seed;

public static class SeedEnumExtension
{
    public static async Task SeedEnums(this AppDbContext dbContext)
    {
        await dbContext.AccountTypes.AddOrUpdateEnumValues<AccountType, AccountTypeEnum>(
            @enum => new AccountType(@enum));
        await dbContext.TransactionStatuses.AddOrUpdateEnumValues<TransactionStatus, TransactionStatusEnum>(
            @enum => new TransactionStatus(@enum));
        await dbContext.AccountStatuses.AddOrUpdateEnumValues<AccountStatus, AccountStatusEnum>(
            @enum => new AccountStatus(@enum));
        await dbContext.TransactionsTypes.AddOrUpdateEnumValues<TransactionType, TransactionTypeEnum>(
            @enum => new TransactionType(@enum));
    }
}