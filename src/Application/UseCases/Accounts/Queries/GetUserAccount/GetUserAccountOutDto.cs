using Core.Common.Bases;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public record GetUserAccountOutDto : BaseDto<Account, GetUserAccountOutDto>
{
    public long Id { get; set; }
    
    public long CustomerId { get; set; }
    
    public decimal Balance { get; set; } = 0;
    
    public List<TransactionOutDto> Transactions { get; set; }

    public record TransactionOutDto
        : BaseDto<Transaction, TransactionOutDto>
    {
        public long Id { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }
        
        public override void AddCustomMappings()
        {
            SetCustomMappings().
                Map(x => x.DateCreated, y => y.Date);
        }
    }

    public override void AddCustomMappings()
    {
        
    }
}