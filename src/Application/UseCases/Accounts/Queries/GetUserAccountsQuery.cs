using Core.UseCases.Accounts.Queries.Responses;
using MediatR;

namespace Core.UseCases.Accounts.Queries;

public class GetUserAccountsQuery: IRequest<List<AccountDto.UserAccountsGet>>
{
    public int UserId { get; set; }
}