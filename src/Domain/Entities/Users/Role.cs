using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User;

public class Role: IdentityRole
{
    public ICollection<UserRole> Users { get; set; }
}