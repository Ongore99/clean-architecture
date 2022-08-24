using Domain.Entities.Accounts;

namespace Infrastructure.Persistence.Seed;

public static class SeedAccountExtension
{
    public static async Task SeedAccount(this AppDbContext dbContext)
    {
        var accounts = new List<Account>()
        {
            new()
            {
                Balance = 2000.0m,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "Description",
                CustomerId = 1
            },
            new()
            {
                Balance = 100.0m,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "Some Description",
                CustomerId = 1
            },
            new()
            {
                Balance = 1.0m,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "One More Description",
                CustomerId = 1
            }
        };

        foreach (var account in accounts)
        {
            var entity = dbContext.Accounts
                .FirstOrDefault(y => y.Balance == account.Balance);
            if (entity is null)
            {
                await dbContext.Accounts.AddAsync(account);
            }
        }
    }
    
}