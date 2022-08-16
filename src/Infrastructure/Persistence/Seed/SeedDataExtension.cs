namespace Infrastructure.Persistence.Seed;

public static class SeedDataExtension
{
    public static async Task SeedData(this AppDbContext dbContext)
    {
        await dbContext.SeedAccount();
    }
}