using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Common.Bases;

namespace Domain.Entities.Transactions;

public class TransactionStatus : EnumTable<TransactionStatusEnum>
{
    public TransactionStatus(TransactionStatusEnum enumType) : base(enumType)
    {
    }
    
    public TransactionStatus() : base() { }
}

public enum TransactionStatusEnum : int
{
    [Display(Name = "Success")]
    Success = 1,
    [Display(Name = "Failed")]
    Failed = 2
}