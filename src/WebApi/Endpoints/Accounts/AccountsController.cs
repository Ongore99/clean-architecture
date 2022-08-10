using System.Net.Mime;
using Core.UseCases.Accounts.Commands.Withdraw;
using Core.UseCases.Accounts.Queries.GetUserAccount;
using Domain.Entities.Accounts;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Common.Extensions;
using WebApi.Endpoints.Accounts.Dtos.Requests;
using WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

namespace WebApi.Endpoints.Accounts;

[Route("accounts")]
[Consumes("application/json")]
public class AccountController : Controller
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
    [ProducesResponseType(typeof(IEnumerable<UserAccountsGetOut>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<UserAccountsGetOut>>> Me()
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
    [HttpPut("{accountId:int}/withdraw")]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [SwaggerRequestExample(typeof(WithdrawRequestDto), typeof(WithdrawExamples))]
    public async Task<ActionResult<Account>> Withdraw([FromBody] WithdrawRequestDto dto,[FromRoute] int accountId
        ,[FromServices] IValidator<WithdrawRequestDto> validator)
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
}