using EFCore.BulkExtensions;

namespace Infrastructure.Persistence.Seed;

public static class SeedDataExtension
{
    public static async Task SeedData(this AppDbContext dbContext)
    {
        await dbContext.SeedEnums();
        await dbContext.SeedUser();
        await dbContext.SeedAccount();
        await dbContext.SeedTransaction();
        await dbContext.SaveChangesAsync();
    }
}