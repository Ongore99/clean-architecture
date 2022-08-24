using System.ComponentModel.DataAnnotations;
using Domain.Common.Interfaces;
using Domain.Entities.Transactions;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Accounts;

public class Account : IIdHas<long>
{
    public long Id { get; set; }

    [Precision(8, 1)] 
    public decimal Balance { get; set; } = 0;

    public DateTime OpenedDate { get; set; } = DateTime.Now;

    public int AccountStatusId { get; set; } = 1;

    public int AccountTypeId { get; set; }
    
    [MaxLength(256)]
    public string Description { get; set; }
    
    public long CustomerId { get; set; }

    public bool IsDeleted { get; set; } = false;
    
    public AccountStatus AccountStatus { get; set; }
    
    public AccountType AccountType { get; set; }
    
    public User Customer { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; }
}