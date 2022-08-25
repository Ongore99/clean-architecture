using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class SeedUserExtension
{
    public static async Task SeedUser(this AppDbContext dbContext)
    {
        var users = new List<User>()
        {
            new()
            {
                EmailConfirmed = true, 
                UserName = "Admin",  
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,  
                PhoneNumber = "1234567890"  
            },
            new()
            {
                UserName = "Employee",  
                Email = "employee@gmail.com",  
                NormalizedUserName = "EMPLOYEE@GMAIL.COM",
                LockoutEnabled = false,  
                PhoneNumber = "1234567890",
                EmailConfirmed = true, 
            },
        };

        foreach (var user in users)
        {
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "Abc1234!");
            
            var entity = await dbContext.Users
                .FirstOrDefaultAsync(y => y.Email == user.Email);
            if (entity is null)
            {
                await dbContext.Users.AddAsync(user);
            }
        }
    }
    
}