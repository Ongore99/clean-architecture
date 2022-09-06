using Domain.Common.Constants;
using Domain.Common.Resources.SharedResource;
using Microsoft.Extensions.Localization;

namespace Domain.Common.Exceptions.DomainExceptions;

public class TransferAccountLimitExceededException : DomainException
{
    public TransferAccountLimitExceededException(decimal balance, 
        IStringLocalizer<SharedResource> _localizer) : 
        base(_localizer[ResxKey.AccountTransferExceeded, balance], 3)
    {
    }
}