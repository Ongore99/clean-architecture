# Architecture
The Projects tries to follow clean architecture with CQRS pattern. CQRS is done using Mediatr library. Here is digram of architecture: <br> <br>
![Clean-Architecture-Diagram2](https://user-images.githubusercontent.com/31799470/189880565-38ff27a4-fbb2-4e4c-8d8b-b5c108c29a05.png)

The **WebApi Project** is  **Infrastructure** is dependent on **Domain and Application** projects. **Application** is dependent on Domain Project.
## Usefull links 
https://www.youtube.com/watch?v=tLk4pZZtiDY&t=379s
# Solution Structure
## Conventions
- All RequestDto should be named with prefix RequestDto:```TransferRequestDto``` <br>
- All Commands should be named with prefix Command:```WithdrawCommand```<br>
- All Queries should be named with prefix Query:```GetUserAccountQuery```<br>
- All Response from Commands and Queries should be named with prefix OutDto:```GetUserAccountOutDto```<br>
- All folders should be created using plural form.<br>
```diff
+ Correct: Extensions
- Incorrect: Extension
```
## Responsiblity of each layer
### **WebApi Project** 
Depends on **Application, Domain and Infrastructure Projects.**

Consists of *Common* and *Endpoints* Folders. <br><br> 
- **Common folder** contains all Common things related our API (For example you may create folders for Extensions, Bases, Attributes, Helpers, Filters, Middlewares, Resources). <br><br>
- **Endpoints folder** contains Controllers, Request Dtos with their Validaitons, Swagger Examples  (Divided based on entity that you are mainly working on) <br><br>

Contains Swagger, Request DTOs for binding, [Swagger Examples](https://medium.com/@niteshsinghal85/multiple-request-response-examples-for-swagger-ui-in-asp-net-core-864c0bdc6619), Library Configurations, Sending Commands and Queries using Mediatr. <br><br>
 - **Library Configurations Path:** ```src/WebApi/Common/Extensions``` (Ef, Validation, Mediatr and other services cnofigs are here)<br><br>
 - **Validation of Request Dtos:** ```src/WebApi/Endpoints/Accounts/Dtos/Requests``` (Divided based on entity). See example of validation in src/WebApi/Endpoints/Accounts/Dtos/Requests/TransferRequestDto.cs <br><br>
 - **Swagger Response and Request Examples:** ```src/WebApi/Endpoints/Accounts/Dtos/SwaggeExamples``` (Divided based on entity) <br><br>

### **Infrastructure Project** 
Depends on **Application, Domain**.

Consists of *Common* and *Persistence* Folders + any folder for other tasks not related to Business logic. For example, Aunthecation Services, External API Calls, Cloud APIs, File Manipulation Services and others <br><br> 
- **Common folder** contains all Common things related our Infrastructure (For example you may create folders for Extensions, Bases, Attributes, Helpers, Constants, Resources). <br><br>
- **Persistence folder** contains EF Configurations, Migrations, Repositories, UnitOfWork, Seeds (basically DAL) <br><br>

Contains external logic not related to Business logic.

- **Migrations Folder:** ```src/Infrastructure/Persistence/Migrations```<br><br>
- **Fluent API Configs** ```src/Infrastructure/Persistence/Configurations```<br><br>
- **Repositories** ```src/Infrastructure/Persistence/Repositories``` (Feel free to extend BaseRepository) <br><br>
- **Seed**  ```src/Infrastructure/Persistence/Seed```<br><br>

### **Application Project** 
Depends on **Domain.**<br><br>
Consists of *Common* and *Usecases* Folders. <br><br> 
- **Common folder** contains all Common things related our Application (For example you may create folders for Extensions, Bases, Interfaces, Helpers, [Behaviours](https://levelup.gitconnected.com/how-i-upgrade-my-code-style-of-mediatr-pipeline-using-net-6-ed49aca61f47), Resources). <br><br>
- **Usecases folder** contains Commands, Queries, their Handlers, OutDtos <br><br>

Contains mapping logic, using repository to retrieve data, calling domain services.

- **Query Folder:** ```src/Application/UseCases/Accounts/Queries```<br><br>
- **Command Folder** ```src/Application/UseCases/Accounts/Commands```<br><br>

### **Domain Project** 
Depends on some libraries. As little as possible. <br><br>
Consists of *Common*, *Entities* and *Services* Folders. <br><br> 
- **Common folder** contains all Common things related our Application (For example you may create folders for Extensions, Bases, Interfaces, Helpers, Exceptions, Domain Validations, Contracts, Resources). <br><br>
- **Entities folder** contains Entites with Exceptions related to this entities <br><br>
- **Services folder** contains Services that are only working with Entities, external services should be defined in Infrastructure layer.<br><br>

Contains business logic using services and entities. Talks with infrastructure using contracts(interfaces)

- **Entities Folder:** ```src/Domain/Entities``` (Divided based on schema) <br><br>
- **Services Folder:** ```src/Domain/Services``` <br><br>
- **Domain Validation Folder** ```src/Domain/Common/Validations```<br><br>
- **Resources Folder** ```src/Domain/Common/Resources```<br><br>
- **Common Exceptions Folder** ```src/Domain/Common/Exceptions```<br><br>
- **Contracts Folder** ```src/Domain/Common/Contracts``` (Infrastructure are using those interfaces to implement them)<br><br>
- **Constants Folder** ```src/Domain/Common/Constants```<br><br>

## Technical Details
### API Design
REST Best practices are using on the current template while designing current API. More about best practices you may found [here](https://soshace.com/rest-api-design-best-practices/).
### Error Handling
For error handling I used popular library [Hellang.Middleware.ProblemDetails](https://www.nuget.org/packages/Hellang.Middleware.ProblemDetails/). <br><br> This library allows to return exception in acceptable [RFC 7807](https://www.rfc-editor.org/rfc/rfc7807) format called **Problem Details**. I created 5 common Exceptions and mapped them to the appropriate Status Codes located in the file: ```src/WebApi/Common/Extensions/ErrorHandlingServices/ErrorHandlingServiceExtension.cs```. Feel Free to modify this file for writing mappings for your exceptions. I also extended built-in Problem Details model to include error codes in response to the error: ```src/WebApi/Common/Extensions/ErrorHandlingServices/CustomProblemDetails.cs```<br>

### Validation
[Fluent Validation Library](https://fluentvalidation.net/) used in this template for request dto validation. Validator logic contains in Request Dto file itself. Fluent Validation [integrated with Swagger](https://anexinet.com/blog/asp-net-core-fluentvalidation-swagger/) to show what validation the Dto has:
```
using System.Text.Json.Serialization;
using FluentValidation;

namespace WebApi.Endpoints.Accounts.Dtos.Requests;

public class TransferRequestDto
{
    [JsonIgnore]
    public int AccountSenderId { get; set; }
    
    public int AccountReceiverId { get; set; }
    
    public decimal Amount { get; set; }
}

public class TransferRequestValidator : AbstractValidator<TransferRequestDto> 
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.AccountReceiverId)
            .GreaterThan(0)
            .NotEqual(x => x.AccountSenderId)
            .WithErrorCode("5");;

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithErrorCode("4");
    }
}
```
<br>
Domain Project is also using Fluent Validation to Validate Entities before do any manipulation with them. Use Domain validation to write common rules for some entities. For example: Before do manipulation with account, check whether the balance is not < 0.<br><br>

Configs are here:```src/WebApi/Common/Extensions/FluentValidationServices/FluentValidationServiceExtension.cs```

### Mediatr
[Mediatr](https://github.com/jbogard/MediatR) used in this template to implement [CQRS Pattern](https://www.youtube.com/watch?v=YzOBrVlthMk). Here is diagram of CQRS Pattern:<br><br>
![image](https://user-images.githubusercontent.com/31799470/190372074-4f267cef-21f1-47e7-bebf-f34e125b283d.png)
<br><br>
**Note:** Commands and Queries should contain their handlers in the same file:
```
using Domain.Common.Contracts;
using Domain.Entities.Accounts.Exceptions;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Accounts.Commands.Withdraw;

public class WithdrawCommand : IRequest<WithdrawAccountOut>
{
    public int UserId { get; set; }
    
    public decimal Balance { get; set; }
    
    public int AccountId { get; set; }
}

public class WithdrawHandler : IRequestHandler<WithdrawCommand, WithdrawAccountOut>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public WithdrawHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<WithdrawAccountOut> Handle(WithdrawCommand cmd, CancellationToken cancellationToken)
    {
        var account = await _unit.AccountRepository
            .GetUserAccount(cmd.UserId, cmd.AccountId);

        await _accountService.Withdraw(account, cmd.Balance);
        await _unit.AccountRepository.Update(account, true);
        
        var withdrawAccount = WithdrawAccountOut.MapFrom(account);
        
        return withdrawAccount;
    }
}
```
You send Commands and Queries using Mediatr in your controller in this way: ```await _mediator.Send(command);```<br>
<br>
You can also add [behaviours](https://youtu.be/2JzQuIvxIqk) to your mediatr request if you wish. 
<br><br>

Configs are here:```src/WebApi/Common/Extensions/MediatrServices/MediatrServiceExtension.cs```

