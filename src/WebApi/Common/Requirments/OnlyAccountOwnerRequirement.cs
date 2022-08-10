using Microsoft.AspNetCore.Authorization;
using WebApi.Common.Services;

namespace WebApi.Common.Requirments;

public class OnlyAccountOwnerRequirement : IAuthorizationRequirement
{
}

public class MinimumAgeHandler : AuthorizationHandler<OnlyAccountOwnerRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, OnlyAccountOwnerRequirement requirement)
    {
        var currentUserId = UserService.GetCurrentUser();
        
        
        return Task.CompletedTask;
    }
}