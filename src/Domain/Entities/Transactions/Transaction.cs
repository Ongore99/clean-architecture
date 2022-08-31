using Domain.Common.Interfaces;
using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Transactions;

public class Transaction : IIdHas<long>
{
    public long Id { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    [Precision(8, 1)] 
    public decimal Amount { get; set; } 
        
    public long AccountId { get; set; }
    
    public int TransactionStatusId { get; set; }
    
    public int TransactionTypeId { get; set; }
    
    public TransactionStatus TransactionStatus { get; set; }
    
    public TransactionType TransactionType { get; set; }
    
    public Account Account { get; set; }
}