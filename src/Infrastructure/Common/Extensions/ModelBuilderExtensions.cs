using Domain.Entities.Accounts;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions;

public static class ModelBuilderExtensions
{
    public static void AddIsDeletedQuery(this ModelBuilder builder)
    {
        builder.Entity<Account>()
            .HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<User>()
            .HasQueryFilter(p => !p.IsDeleted);
    }  
}