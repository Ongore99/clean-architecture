using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class Role : IdentityRole<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}