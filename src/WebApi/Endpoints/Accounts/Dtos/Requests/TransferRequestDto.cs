using System.Text.Json.Serialization;
using FluentValidation;

namespace WebApi.Endpoints.Accounts.Dtos.Requests;

public class TransferRequestDto
{
    [JsonIgnore]
    public int AccountSenderId { get; set; }
    
    public int AccountReceiverId { get; set; }
    
    public decimal Amount { get; set; }
}

public class TransferRequestValidator : AbstractValidator<TransferRequestDto> 
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.AccountReceiverId)
            .GreaterThan(0)
            .NotEqual(x => x.AccountSenderId);;
        RuleFor(x => x.Amount).GreaterThan(0).LessThan(10000);
    }
}