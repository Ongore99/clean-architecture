using System.Text.Json.Serialization;
using FluentValidation;
using WebApi.Common.Services;

namespace WebApi.Endpoints.Accounts.Dtos.Requests;


public class WithdrawRequestDto
{
    public decimal Balance { get; set; }

    [JsonIgnore]
    public int AccountId { get; set; }
    
    [JsonIgnore]
    public int UserId => UserService.GetCurrentUser();
}

public class WithdrawRequestValidator : AbstractValidator<WithdrawRequestDto> 
{
    public WithdrawRequestValidator()
    {
        RuleFor(x => x.Balance).GreaterThan(0);
        RuleFor(x => x.AccountId).GreaterThan(0);
    }
}