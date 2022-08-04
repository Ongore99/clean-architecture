using Core.Common.Contracts;
using Core.UseCases.Accounts.Queries.Responses;
using Domain.Entities.Accounts;
using MediatR;

namespace Core.UseCases.Accounts.Queries.Handlers;

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountsQuery, List<AccountDto.UserAccountsGet>>
{
    public IRepositoryBase<Account> RepositoryBase;

    public GetUserAccountsHandler(IRepositoryBase<Account> repositoryBase)
    {
        RepositoryBase = repositoryBase;
    }

    public Task<List<AccountDto.UserAccountsGet>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}