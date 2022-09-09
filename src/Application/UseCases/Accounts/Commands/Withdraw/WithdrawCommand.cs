using Domain.Common.Contracts;
using Domain.Entities.Accounts.Exceptions;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public class WithdrawCommand : IRequest<WithdrawAccountOut>
{
    public int UserId { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountId { get; set; }
}

public class WithdrawHandler : IRequestHandler<WithdrawCommand, WithdrawAccountOut>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public WithdrawHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<WithdrawAccountOut> Handle(WithdrawCommand cmd, CancellationToken cancellationToken)
    {
        var account = await _unit.AccountRepository
            .GetUserAccount(cmd.UserId, cmd.AccountId);

        await _accountService.Withdraw(account, cmd.Balance);
        await _unit.AccountRepository.Update(account, true);
        
        var withdrawAccount = WithdrawAccountOut.MapFrom(account);
        
        return withdrawAccount;
    }
}