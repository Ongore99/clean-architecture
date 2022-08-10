using Domain.Common.Contracts;
using Domain.Entities.Accounts;
using MediatR;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public class WithdrawCommand: IRequest<Account>
{
    public int UserId { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountId { get; set; }
}

public class GetUserAccountsHandler : IRequestHandler<WithdrawCommand, Account>
{
    private IRepositoryBase<Account> RepositoryBase;

    public GetUserAccountsHandler(IRepositoryBase<Account> repositoryBase)
    {
        RepositoryBase = repositoryBase;
    }

    public async Task<Account> Handle(WithdrawCommand cmd, CancellationToken cancellationToken)
    {
        var account = await RepositoryBase
            .ByIdAsync(cmd.AccountId);


        return account;
    }
}