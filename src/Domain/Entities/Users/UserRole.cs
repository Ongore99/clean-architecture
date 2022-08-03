using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }

    public Role Role { get; set; }
}
