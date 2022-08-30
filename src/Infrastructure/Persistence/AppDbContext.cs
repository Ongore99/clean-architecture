using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Domain.Entities.Users;
using Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<User, Role, long,
    IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
    IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public AppDbContext()
    {
            
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
        
    public DbSet<User> Users { get; set; }
    
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<AccountType> AccountTypes { get; set; }
    
    public DbSet<AccountStatus> AccountStatuses { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
    
    public DbSet<TransactionStatus> TransactionStatuses { get; set; }

    public DbSet<TransactionType> TransactionsTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        foreach (var x in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            x.DeleteBehavior = DeleteBehavior.ClientCascade;
        
        builder.AddIsDeletedQuery();
    }
}