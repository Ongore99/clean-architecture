using Core.Common.Contracts;
using Core.UseCases.Users.Commands;
using Domain.Entities.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;

    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> CreateUserAsync(UserRegisterCommand userRegisterCommand)
    {
        var passwordValidator = new PasswordValidator<User>();
        var passwordValidation = await passwordValidator
            .ValidateAsync(_userManager, null, userRegisterCommand.Password);

        if (!passwordValidation.Succeeded)
        {
            return passwordValidation;
        }

        var user = userRegisterCommand.Adapt<User>();
        var result = await _userManager.CreateAsync(user);

        return result;
    }
}