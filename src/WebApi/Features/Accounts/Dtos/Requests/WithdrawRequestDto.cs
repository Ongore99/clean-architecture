using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Features.Accounts.Dtos.Requests;


public class WithdrawRequestDto
{
    [FromBody]
    public decimal Balance { get; set; }

    [JsonIgnore]
    [FromRoute]
    public int AccountId { get; set; }
    
    [JsonIgnore]
    [FromBody]
    public int UserId { get; set; }
}

public class WithdrawRequestValidator : AbstractValidator<WithdrawRequestDto> 
{
    public WithdrawRequestValidator() 
    {
        RuleFor(x => x.Balance).LessThanOrEqualTo(0);
        RuleFor(x => x.AccountId).LessThanOrEqualTo(0);
    }
}