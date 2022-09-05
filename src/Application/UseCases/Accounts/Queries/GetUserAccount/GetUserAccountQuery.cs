using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public class GetUserAccountQuery : IRequest<GetUserAccountOutDto>
{
    public GridifyQuery Query { get; set; }
    
    public int AccountId { get; set; }
    
    public int UserId { get; set; }
}

public class GetUserAccountsHandler : IRequestHandler<GetUserAccountQuery, GetUserAccountOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetUserAccountsHandler(IUnitOfWork unitOfWork)
    {
        _unit = unitOfWork;
    }

    public async Task<GetUserAccountOutDto> Handle(GetUserAccountQuery request,
        CancellationToken cancellationToken)
    {
        var account = await _unit.AccountRepository
            .FirstToAsync<GetUserAccountOutDto>
                (x => x.CustomerId == request.UserId && x.Id == request.AccountId);
        
        account.Transactions = _unit.TransactionRepository
            .FindByCondition<GetUserAccountOutDto.TransactionOutDto>
                (x => x.AccountId == request.AccountId)
            .GridifyQueryable(request.Query);
        
        return account;
    }
}