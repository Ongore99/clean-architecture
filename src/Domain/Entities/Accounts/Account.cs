using System.ComponentModel.DataAnnotations;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Accounts;

public class Account: IIdHas<long>
{
    public long Id { get; set; }

    [Precision(8, 4)] 
    public decimal Balance { get; set; } = 0;
    
    public int AccountStatusId { get; set; }
    
    public int AccountTypeId { get; set; }
    
    [MaxLength(256)]
    public string Description { get; set; }
}