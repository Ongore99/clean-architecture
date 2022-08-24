using System.ComponentModel;
using Domain.Common.Bases;

namespace Domain.Entities.Transactions;

public class TransactionType : EnumTable<TransactionTypeEnum>
{
    public TransactionType(TransactionTypeEnum enumType) : base(enumType)
    {
    }

    public TransactionType() : base() { }
}

public enum TransactionTypeEnum : int
{
    [Description("Purchase was made")]
    Purchase = 1,
    [Description("Money Transfer")]
    Transfer = 2
}