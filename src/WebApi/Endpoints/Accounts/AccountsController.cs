using System.Net.Mime;
using Core.UseCases.Accounts.Commands.Withdraw;
using Core.UseCases.Accounts.Queries.GetUserAccount;
using Core.UseCases.Accounts.Queries.GetUserAccounts;
using Domain.Entities.Accounts;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Common.Extensions;
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
    [EnableQuery(PageSize = 10)]
    [HttpGet("me")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<UserAccountsGetOutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<UserAccountsGetOutDto>>> Me()
    {
        // TODO: REMOVE
        var currentUserId = 1;
        var query = new GetUserAccountsQuery()
        {
            UserId = currentUserId
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
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
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
    /// Get Account by id
    /// </summary>
    /// <returns>New Updated Account</returns>
    /// <response code="200">New Updated Account</response>
    [HttpGet("{accountId:int}")]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(typeof(GetUserAccountOutDto), StatusCodes.Status200OK)]
    [SwaggerResponseExample(200, typeof(GetAccountResponseExamples))]
    public async Task<ActionResult<GetUserAccountOutDto>> ById(
        [FromRoute] int accountId, [FromQuery] GridifyQuery q)
    {
        var query = new GetUserAccountQuery()
        {
            AccountId = accountId,
            UserId = UserService.GetCurrentUser(),
            Query = q
        };
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }
}