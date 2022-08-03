using Core.UseCases.Accounts.Queries.Responses;
using MediatR;

namespace Core.UseCases.Accounts.Queries.Handlers;

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountsQuery, List<AccountDto.UserAccountsGet>>
{
    public Task<List<AccountDto.UserAccountsGet>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}