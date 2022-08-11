using Domain.Common.Contracts;
using Domain.Entities.Accounts;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public class WithdrawCommand: IRequest<Account>
{
    public int UserId { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountId { get; set; }
}

public class WithdrawHandler : IRequestHandler<WithdrawCommand, Account>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountService _accountService;

    public WithdrawHandler(IAccountRepository accountRepository, IAccountService accountService)
    {
        _accountRepository = accountRepository;
        _accountService = accountService;
    }

    public async Task<Account> Handle(WithdrawCommand cmd, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetUserAccount(cmd.UserId, cmd.AccountId);

        await _accountService.Withdraw(account, cmd.Balance);
        await _accountRepository.Update(account, true);
        
        return account;
    }
}