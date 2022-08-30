namespace Core.UseCases.Accounts.Queries.GetUserAccounts;

public class UserAccountsGetOut
{
    public long Id { get; set; }

    public decimal Balance { get; set; }

    public int AccountStatusId { get; set; }

    public int AccountTypeId { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }
}