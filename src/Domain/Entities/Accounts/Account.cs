using Domain.Common.Interfaces;

namespace Domain.Entities.Accounts;

public class Account: IIdHas<long>
{
    public long Id { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountStatusId { get; set; }
    
    public int AccountTypeId { get; set; }
    
    public string Description { get; set; }
}