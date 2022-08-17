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
                Balance = 20000,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "Description"
            },
            new()
            {
                Balance = 100,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "Some Description"
            },
            new()
            {
                Balance = 1,
                AccountStatusId = 1,
                AccountTypeId = 1,
                Description = "One More Description"
            }
        };

        accounts.ForEach(x =>
        {
            var entity = dbContext.Accounts.FirstOrDefault(y => y.Balance == x.Balance);
            if (entity is null)
            {
                dbContext.AddAsync(entity);
            }
        });
    }
    
}