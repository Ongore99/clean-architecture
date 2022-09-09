using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .Property(b => b.AccountStatusId)
            .HasDefaultValue(1);

        builder
            .Property(b => b.IsDeleted)
            .HasDefaultValue(false);
        
        builder.HasOne(x => x.AccountType)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.AccountTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.AccountStatus)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.AccountStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}