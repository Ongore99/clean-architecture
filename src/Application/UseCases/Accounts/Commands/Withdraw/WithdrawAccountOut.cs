
using Core.Common.Bases;
using Domain.Entities.Accounts;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public record WithdrawAccountOut : BaseDto<Account, WithdrawAccountOut>
{
    public long Id { get; set; }

    public decimal Balance { get; set; }

    public DateTime OpenedDate { get; set; }

    public int AccountStatusId { get; set; }

    public int AccountTypeId { get; set; }
    
    public string Description { get; set; }
    
    public long CustomerId { get; set; }
}