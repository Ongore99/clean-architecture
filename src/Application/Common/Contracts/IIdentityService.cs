using Core.UseCases.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace Core.Common.Contracts;

public interface IIdentityService
{
    public Task<IdentityResult> CreateUserAsync(UserRegisterCommand userRegisterCommand);
}