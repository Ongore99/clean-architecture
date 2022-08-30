using Core.Common.Bases;
using Domain.Entities.Accounts;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public record GetUserAccountOutDto : BaseDto<Account, GetUserAccountOutDto>
{
    public long Id { get; set; }
    
    public long CustomerId { get; set; }
    
    public decimal Balance { get; set; } = 0;
    
    public List<TransactionOutDto> Transactions { get; set; }

    public record TransactionOutDto(long Id, decimal Amount, DateTime Date);

    public override void AddCustomMappings()
    {
        SetCustomMappings().
            Map(x => x.Transactions, y => y.Transactions);
    }
}