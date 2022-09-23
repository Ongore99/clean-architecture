using Core.Common.Contracts;
using Domain.Common.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Core.UseCases.Users.Commands;

public class UserRegisterCommand : IRequest<IdentityResult>
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string UserName { get; init; }
    
    public string Password { get; init; }
    
    public string Email { get; init; }
    
    public string PhoneNumber { get; init; }
}

public class UserCommandHandler : IRequestHandler<UserRegisterCommand, IdentityResult>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unit;
    private readonly ILogger<UserCommandHandler> _logger;

    public UserCommandHandler(IIdentityService identityService, 
        IUnitOfWork unit,
        ILogger<UserCommandHandler> logger)
    {
        _identityService = identityService;
        _unit = unit;
        this._logger = logger;
    }

    public async Task<IdentityResult> Handle(UserRegisterCommand cmd, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(cmd);

        return result;
    }
}