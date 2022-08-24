using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class UserRole : IdentityUserRole<long>
{
    public long UserId { get; set; }
    
    public long RoleId { get; set; }
    
    public User User { get; set; }

    public Role Role { get; set; }
}
