using Domain.Entities.Users;

namespace WebApi.Common.Extensions.IdentityServices;

public static class IdentityServiceExtension
{
    internal static void AddIdentityService(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.AllowedForNewUsers = true;
            
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });
    }
}