using System.ComponentModel;
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
    public IActionResult Me()
    {
        
        return Ok();
    }
}