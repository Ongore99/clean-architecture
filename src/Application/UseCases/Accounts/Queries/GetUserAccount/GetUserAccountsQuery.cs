using Domain.Common.Contracts;
using Domain.Entities.Accounts;
using MediatR;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public class GetUserAccountsQuery: IRequest<IQueryable<UserAccountsGetOut>>
{
    public int UserId { get; set; }
}

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountsQuery, IQueryable<UserAccountsGetOut>>
{
    private IRepositoryBase<Account> RepositoryBase;

    public GetUserAccountsHandler(IRepositoryBase<Account> repositoryBase)
    {
        RepositoryBase = repositoryBase;
    }

    public async Task<IQueryable<UserAccountsGetOut>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await RepositoryBase
            .FindByConditionToType<UserAccountsGetOut>(x => x.UserId == request.UserId);
        
        return accounts;
    }
}