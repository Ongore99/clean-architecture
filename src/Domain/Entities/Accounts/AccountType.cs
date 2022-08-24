using System.ComponentModel;
using Domain.Common.Bases;

namespace Domain.Entities.Accounts;

public class AccountType : EnumTable<AccountTypeEnum>
{
    public ICollection<Account> Accounts { get; set; }
    
    public AccountType(AccountTypeEnum enumType) : base(enumType)
    {
    }

    public AccountType() : base() { }
}

public enum AccountTypeEnum : int
{
    [Description("Classic account holders")]
    Classic = 1,
    [Description("Premium account holders")]
    Premium = 2
}