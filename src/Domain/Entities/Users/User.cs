using Domain.Entities.Accounts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class User : IdentityUser<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
    
    public ICollection<Account> Accounts { get; set; }

    public bool IsDeleted { get; set; } = false;
}
