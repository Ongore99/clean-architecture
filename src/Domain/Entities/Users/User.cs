using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class User: IdentityUser
{
    public ICollection<UserRole> Roles { get; set; }
}
