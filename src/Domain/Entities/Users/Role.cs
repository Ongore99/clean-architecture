using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class Role: IdentityRole
{
    public ICollection<UserRole> Users { get; set; }
}