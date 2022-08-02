using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WebApi.Features.Accounts;

[Route("accounts")]
public class AccountController : Controller
{
    
    [EnableQuery]
    [HttpGet("/me")]
    public IActionResult Me()
    {
        return Ok();
    }
}