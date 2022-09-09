using Domain.Common.Constants;
using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Accounts.Exceptions;

public class TransferAccountLimitExceededException : DomainException
{
    public TransferAccountLimitExceededException(AccountTypeEnum accountAccountTypeId, decimal balance,
        IStringLocalizer<AccountResource> _localizer) : 
            base(_localizer[ResxKey.AccountTransferExceeded, accountAccountTypeId, balance], 3)
    {
    }
}