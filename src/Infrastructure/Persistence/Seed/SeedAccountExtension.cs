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
                Description = "S"
            }
        }
    }
    
}