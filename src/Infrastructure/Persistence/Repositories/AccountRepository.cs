using Domain.Entities.Accounts;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories;

public class AccountRepository: RepositoryBase<Account>
{
    public AccountRepository(AppDbContext repositoryContext) : base(repositoryContext)
    {
    }
}