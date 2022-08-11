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
    private readonly IBaseRepository<Account> _baseRepository;

    public GetUserAccountsHandler(IBaseRepository<Account> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IQueryable<UserAccountsGetOut>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _baseRepository
            .FindByConditionToType<UserAccountsGetOut>(x => x.UserId == request.UserId);
        
        return accounts;
    }
}