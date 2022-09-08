using System.Net;
using System.Net.Mime;
using Core.UseCases.Accounts.Commands.Transfer;
using Core.UseCases.Accounts.Commands.Withdraw;
using Core.UseCases.Accounts.Queries.GetUserAccount;
using Core.UseCases.Accounts.Queries.GetUserAccounts;
using Domain.Entities.Accounts;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Common.Services;
using WebApi.Endpoints.Accounts.Dtos.Requests;
using WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

namespace WebApi.Endpoints.Accounts;

[Route("api/accounts")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class AccountController : BaseController
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// List of my accounts
    /// </summary>
    [HttpGet("me")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<UserAccountsGetOutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<UserAccountsGetOutDto>>> Me()
    {
        var query = new GetUserAccountsQuery()
        {
            UserId = UserService.GetCurrentUser()
        };

        var result = await _mediator.Send(query);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Withdraw from my account
    /// </summary>
    /// <returns>New Updated Account</returns>
    /// <response code="200">New Updated Account</response>
    [HttpPatch("{accountId:int}/withdraw")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(WithdrawAccountOut), StatusCodes.Status200OK)]
    [SwaggerRequestExample(typeof(WithdrawRequestDto), typeof(WithdrawExamples))]
    public async Task<ActionResult<Account>> Withdraw(
        [FromBody] WithdrawRequestDto dto,
        [FromRoute] int accountId,
        [FromServices] IValidator<WithdrawRequestDto> validator)
    {
        dto.AccountId = accountId;
        
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        
        var command = dto.Adapt<WithdrawCommand>();
        
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Transfer balance from one account to another
    /// </summary>
    /// <response code="200"></response>
    [HttpPatch("{accountId:int}/transfer")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerRequestExample(typeof(TransferRequestDto), typeof(TransferRequestExamples))]
    public async Task<ActionResult<HttpStatusCode>> Transfer(
        [FromBody] TransferRequestDto dto,
        [FromRoute] int accountId,
        [FromServices] IValidator<TransferRequestDto> validator)
    {
        dto.AccountSenderId = accountId;
        
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        
        var command = dto.Adapt<TransferCommand>();
        
        var result = await _mediator.Send(command);
        
        return result;
    }
    
    /// <summary>
    /// Get Account by id
    /// </summary>
    /// <returns>New Updated Account</returns>
    /// <response code="200">New Updated Account</response>
    [HttpGet("{accountId:int}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetUserAccountOutDto), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(GetAccountResponseExamples))]
    public async Task<ActionResult<GetUserAccountOutDto>> ById(
        [FromRoute] int accountId, 
        [FromQuery] GridifyQuery query)
    {
        var getUserAccountQuery = new GetUserAccountQuery()
        {
            AccountId = accountId,
            UserId = UserService.GetCurrentUser(),
            Query = query
        };
        
        var result = await _mediator.Send(getUserAccountQuery);
        
        return Ok(result);
    }
}