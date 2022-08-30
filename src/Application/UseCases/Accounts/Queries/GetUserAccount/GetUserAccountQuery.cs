using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public class GetUserAccountQuery : IRequest<IQueryable<GetUserAccountOutDto>>
{
    public int AccountId { get; set; }
    
    public int UserId { get; set; }
}

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountQuery, IQueryable<GetUserAccountOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetUserAccountsHandler(IUnitOfWork unitOfWork)
    {
        _unit = unitOfWork;
    }

    public async Task<IQueryable<GetUserAccountOutDto>> Handle(GetUserAccountQuery request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var accounts = _unit.AccountRepository
            .FindByCondition<GetUserAccountOutDto>
                (x => x.CustomerId == request.UserId && x.Id == request.AccountId,
                    include: x => x.Include(y => y.Transactions));
        
        return accounts;
    }
}