using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.UseCases.Accounts.Commands.Transfer;

public class TransferCommand : IRequest<HttpStatusCode>
{
    public int AccountSenderId { get; set; }
    
    public int AccountReceiverId { get; set; }
    
    public decimal Amount { get; set; }
    
    public int UserId { get; set; }
}

public class TransferCommandHandler : IRequestHandler<TransferCommand, HttpStatusCode>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;
    private readonly ILogger<TransferCommandHandler> _logger;

    public TransferCommandHandler(IAccountService accountService, 
        IUnitOfWork unit,
        ILogger<TransferCommandHandler> _logger)
    {
        _accountService = accountService;
        _unit = unit;
        this._logger = _logger;
    }

    public async Task<HttpStatusCode> Handle(TransferCommand cmd, CancellationToken cancellationToken)
    {
        await _unit.BeginTransactionAsync();
        try
        {
            var account = await _unit.AccountRepository
                .FirstAsync(x => x.Id == cmd.AccountSenderId);
            
            var receiverAccount = await _unit.AccountRepository
                .FirstAsync(x => x.Id == cmd.AccountReceiverId);
            
            await _accountService.Transfer(account, receiverAccount, cmd.Amount);
            
            await _unit.CommitAsync(true);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            await _unit.RollbackAsync();
            throw;
        }

        return HttpStatusCode.OK;
    }
}