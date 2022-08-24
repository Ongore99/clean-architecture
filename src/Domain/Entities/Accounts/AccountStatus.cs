using Domain.Common.Bases;

namespace Domain.Entities.Accounts;

public class AccountStatus : EnumTable<AccountStatusEnum>
{
    public AccountStatus(AccountStatusEnum enumType) : base(enumType)
    {
    }

    public AccountStatus() : base() { }
    
    public ICollection<Account> Accounts { get; set; }
}

public enum AccountStatusEnum
{
    Creating = 1,
    Opened = 2,
    Closed = 3
}