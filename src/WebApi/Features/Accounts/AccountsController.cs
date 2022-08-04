using System.ComponentModel.DataAnnotations;
using Core.UseCases.Accounts.Queries;
using Domain.Entities.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApi.Features.Accounts;

[Route("accounts")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [EnableQuery]
    [HttpGet("/me")]
    public ActionResult<IQueryable<Account>> Me()
    {
        // TODO Remove
        var currentUserId = 1;
        var query = new GetUserAccountsQuery()
        {
            UserId = currentUserId
        };

        var result = _mediator.Send(query);
        
        return Ok(result);
    }
}