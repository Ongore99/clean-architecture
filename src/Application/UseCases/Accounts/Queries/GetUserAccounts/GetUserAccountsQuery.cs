using Domain.Common.Contracts;
using Domain.Entities.Accounts;
using MediatR;

namespace Core.UseCases.Accounts.Queries.GetUserAccounts;

public class GetUserAccountsQuery : IRequest<IQueryable<UserAccountsGetOutDto>>
{
    public int UserId { get; set; }
}

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountsQuery, IQueryable<UserAccountsGetOutDto>>
{
    private readonly IBaseRepository<Account> _baseRepository;

    public GetUserAccountsHandler(IBaseRepository<Account> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IQueryable<UserAccountsGetOutDto>> Handle(GetUserAccountsQuery request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var accounts = _baseRepository
            .FindByCondition<UserAccountsGetOutDto>(x => x.CustomerId == request.UserId);

        return accounts;
    }
}