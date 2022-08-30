using Domain.Common.Contracts;
using Domain.Entities.Accounts;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public class WithdrawCommand : IRequest<Account>
{
    public int UserId { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountId { get; set; }
}

public class WithdrawHandler : IRequestHandler<WithdrawCommand, Account>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public WithdrawHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<Account> Handle(WithdrawCommand cmd, CancellationToken cancellationToken)
    {
        var account = await _unit.AccountRepository
            .GetUserAccount(cmd.UserId, cmd.AccountId);
        
        await _accountService.Withdraw(account, cmd.Balance);
        await _unit.AccountRepository.Update(account, true);
        
        return account;
    }
}