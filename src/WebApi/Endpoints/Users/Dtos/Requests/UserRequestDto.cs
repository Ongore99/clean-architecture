using FluentValidation;
using WebApi.Endpoints.Accounts.Dtos.Requests;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class UserRequestDto
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string UserName { get; init; }
    
    public string Password { get; init; }
    
    public string Email { get; init; }
    
    public string PhoneNumber { get; init; }
}

public class UserRequestValidator : AbstractValidator<UserRequestDto> 
{
    public UserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}