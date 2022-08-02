using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User;

public class User: IdentityUser
{
    public ICollection<UserRole> Roles { get; set; }
}
