using System.Net;
using System.Net.Mime;
using Core.UseCases.Accounts.Queries.GetUserAccounts;
using Core.UseCases.Users.Commands;
using Domain.Entities.Users;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Users;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Status code</response>
    [HttpPost("register", Name = "RegisterUser")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromBody] UserRequestDto userRequestDto,
        [FromServices] IValidator<UserRequestDto> validator)
    {
        var user = userRequestDto.Adapt<UserRegisterCommand>();
        var validation = await validator.ValidateAsync(userRequestDto);
        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var result = await _mediator.Send(user);

        return result.Succeeded ? StatusCode(201) : new BadRequestObjectResult(result);
    }
}