using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Accounts.Commands.Transfer;

public class TransferCommand : IRequest<HttpStatusCode>
{
    public int AccountSenderId { get; set; }
    
    public int AccountReceiverId { get; set; }
    
    public decimal Amount { get; set; }
}

public class TransferCommandHandler : IRequestHandler<TransferCommand, HttpStatusCode>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public TransferCommandHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(TransferCommand cmd, CancellationToken cancellationToken)
    {
        await _unit.BeginTransaction();
        try
        {
            var account = await _unit.AccountRepository
                .FirstAsync(x => x.Id == cmd.AccountSenderId);
            
            var receiverAccount = await _unit.AccountRepository
                .FirstAsync(x => x.Id == cmd.AccountReceiverId);
            
            _accountService.Transfer(account, receiverAccount, cmd.Amount);
            
            await _unit.CommitAsync();
        }
        catch
        {
            await _unit.RollbackAsync();
        }

        return HttpStatusCode.OK;
    }
}