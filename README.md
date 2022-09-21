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

Contains Swagger, Request DTOs for binding, Swagger Examples, Library Configurations, Sending Commands and Queries using Mediatr. <br><br>
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
REST Best practices are using on the current template while designing current API. More about best practices you may found [here](https://soshace.com/rest-api-design-best-practices/).  <br><br>
### Error Handling
For error handling I used popular library [Hellang.Middleware.ProblemDetails](https://www.nuget.org/packages/Hellang.Middleware.ProblemDetails/). <br><br> This library allows to return exception in [RFC 7807](https://www.rfc-editor.org/rfc/rfc7807) standart called **Problem Details**. I created 5 common Exceptions and mapped them to the appropriate Status Codes located in the file: ```src/WebApi/Common/Extensions/ErrorHandlingServices/ErrorHandlingServiceExtension.cs```. Feel Free to modify this file for writing mappings for your exceptions. You should also provide error code in the form of integer. Error code is unique identifier of the exception. I also extended built-in Problem Details model to include error codes in response to the error: ```src/WebApi/Common/Extensions/ErrorHandlingServices/CustomProblemDetails.cs```<br>

Current Base Exceptions:
- [Validation Exception](https://docs.fluentvalidation.net/en/latest/start.html#throwing-exceptions) -> Throws by Fluent Validation. *Mapped to Status Code 400*
- AuthenticationCustomException ```(src/Domain/Common/Exceptions/AuthenticationCustomException.cs)``` -> Should be thrown when user is not auntenticated. *Mapped to Status Code 403*
- AuthorizationException ```(src/Domain/Common/Exceptions/AuthorizationException.cs)``` -> Should be thrown when user is not aunthorized. *Mapped to Status Code 401*
- NotFoundException ```(src/Domain/Common/Exceptions/NotFoundException.cs)``` -> Should be thrown when entity is not found. Use ```First()``` method from ```BaseRepository``` to automatically throw this exception when entity is not found. *Mapped to Status Code 404*
- InnerException ```(src/Domain/Common/Exceptions/InnerException.cs)``` -> Should be thrown when something unexpected happen and should not be dislpayed on the screen. *Mapped to Status Code 500*
- DomainException ```(src/Domain/Common/Exceptions/DomainException.cs)``` -> Should be thrown when something expected happen and should be dislpayed on the screen. It is abstract class so you need to create specific Business Exception that will inherit DomainException. See example: ```TransferAccountLimitExceededException```. *Mapped to Status Code 400*<br><br>
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

Configs are here:```src/WebApi/Common/Extensions/FluentValidationServices/FluentValidationServiceExtension.cs``` <br><br>

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

Configs are here:```src/WebApi/Common/Extensions/MediatrServices/MediatrServiceExtension.cs``` <br><br>

### Repositories and Unit of Work
[Repository](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) used in this template to encapsulate Data Access. It provides the following benefits:
- Improves Readbility of the code
- Improves Testability of the code
- Reusing popular methods

If this is not convenient for you, you may inject directly DbContext and work with it from Handlers. <br><br>
If you create new repository you should inherit from ```src/Infrastructure/Persistence/Repositories/Base/BaseRepository.cs``` because it provides usefull base methods that you may need. Most of the methods were copy pasted from the open source [library](https://github.com/Arch/UnitOfWork/blob/master/src/UnitOfWork/Repository.cs). Feel free to extend BaseRepository to have additional methods for your need.
<br><br>
Unit of work is needed only for encapsulation of transactions and to have only one injection instead of injecting all repositories. If you create new repository, add this repository to ```src/Domain/Common/Contracts/IUnitOfWork.cs``` accordingly. To work with repositries inject Unit of Work as follows:
```
public class TransferCommandHandler : IRequestHandler<TransferCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public TransferCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }
}
```
<br><br>
Configs are here: ```src/WebApi/Common/Extensions/RepositoryServices/RepositoryServiceExtension.cs``` <br><br>

### Mapping
[Mapster](https://github.com/MapsterMapper/Mapster) was used for mapping in this project. Here are advantages of the Mapster:
- It is fast
- It is convenient
- It is flexible

I used this [article](https://sd.blackball.lv/articles/read/18850) as an idea for Mapping strategy. To create map config for your dto you need to inherit your Dto to ```BaseDto<TSource, TDest>``` and override ```AddCustomMappings``` method. Example (src/Application/UseCases/Accounts/Queries/GetUserAccount/GetUserAccountOutDto.cs):
```
public record TransactionOutDto
        : BaseDto<Transaction, TransactionOutDto>
    {
        public long Id { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }
        
        public override void AddCustomMappings()
        {
            SetCustomMappings().
                Map(x => x.DateCreated, y => y.Date);
        }
    }
```
<br>

Then it will automatically will use your mapping config when you call *Adapt()* or *ProjectTo()* methods.
<br><br>
Configs are here: ```src/WebApi/Common/Extensions/MapsterServices/MapsterExtension.cs```<br><br>


### Logging
[Serialog](https://serilog.net/) was used for logging in this project. Here are advantages of the Serialog:
- It is convenient
- It is flexible

I used this [video tutorial]([https://sd.blackball.lv/articles/read/18850](https://www.youtube.com/watch?v=_iryZxv8Rxw&t)) as an implementation for logging strategy. All configurations of Gridify comes from appsettings.json file:
```
"Serilog": {
        "Using": [],
        "MinimumLevel": {
            "Default": "Information",
            "Override": {
                "Microsoft": "Warning",
                "System": "Warning"
            }
        },
        "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId"],
        "WriteTo": [
            {
                "Name": "Console"
            }
            /*,{
                "Name": "File",
                "Args": {
                    "path": "E:\\Apps\\Projects\\clean-architecture\\Logs\\log.txt",
                    "outputTemplate": "{Timestamp:G} {Message} {NewLine:1}{Exception:1}"
                }
            }*/
        ]
    },
```
<br>

Feel free to modify configurations per your needs. Currently, it will write all configurations to Console and File(if you uncomment File section) 
<br><br>
Configs are here: ```src/WebApi/Common/Extensions/SerialogServices/SerialogServices.cs```<br><br>

### Pagination, Sorting and Filtering
[Gridify](https://alirezanet.github.io/Gridify/) was used for pagination, sorting and filtering in this project. Here are advantages of the Gridify:
- It is convenient
- It is fast

To implement filtering, sorting and pagination you just need to create query parameter in your controller like this:
```
public async Task<ActionResult<GetUserAccountOutDto>> ById(
        [FromRoute] int accountId, 
        [FromQuery] GridifyQuery query)
    {
        ...
    }
```
<br>

Then you need to call built-in extension method ```.GridifyQueryable(request.Query)``` on your IQuerable result like this:
<br>
```
account.Transactions = _unit.TransactionRepository
            .FindByCondition<GetUserAccountOutDto.TransactionOutDto>
                (x => x.AccountId == request.AccountId)
            .GridifyQueryable(request.Query);
```
<br>
All configs come from appsettings.json
<br>

```
    "GridifyOptions": {
        "DefaultPageSize": 25,
        "AllowNullSearch": true
    },
```

<br>

Feel free to modify configurations per your needs. 
<br><br>
Configs are here: ```src/WebApi/Common/Extensions/GridifyServices/GridifyServiceExtension.cs```<br><br>

### Localization
To implement localization this [article](https://lokalise.com/blog/asp-net-core-localization/#ASPNET_Core_date_and_time_format_localization) was used. The current project supports the following cultures: *en-us, ru-ru*, default culture: *en*. 
<br>
- All ther error messages should be contained in resources, except Inner Exceptions. The texts of InnerExceptions could be located as constants since they don't require to be displayed. 
- Each layer(WebApi, Infrastructure...) should have its own *Resources* folder with texts used in this layer. 
- Each entity should have its own resource file and associated texts inside so we could avoid a lot of text messages in one resource file.
<br>

Configs are here: ```src/WebApi/Common/Extensions/LocalizationServices/LocalizationServiceExtension.cs```<br><br>

### Swagger
To have a good and readble api please use appropriate attributes to decorate swagger. Readble API will decrease amount of explanation to Frontend side.
- [ProducesResponseType(typeof(GetUserAccountOutDto), StatusCodes.Status200OK)] -> shows what object type will be returned in case of status 200
- [ProducesDefaultResponseType(typeof(CustomProblemDetails))] -> shows what object type will be returned in case of any other response
- [Consumes(MediaTypeNames.Application.Json)] -> shows what endpoint can consume from swagger(in this case only JSON)
- [Produces(MediaTypeNames.Application.Json)] -> shows what endpoint can produce from swagger(in this case only JSON)
- [SwaggerRequestExample(typeof(WithdrawRequestDto), typeof(WithdrawExamples))] -> shows request object examples
- [SwaggerResponseExample(typeof(WithdrawRequestDto), typeof(WithdrawExamples))] -> shows response object examples
- Fluent Validation [integrated with Swagger](https://anexinet.com/blog/asp-net-core-fluentvalidation-swagger/) to show what validation the Dto has

Here is example of proper docs of swagger endpoint:
```
 /// <summary>
 /// Transfer balance from one account to another
 /// </summary>
 /// <param name="accountId"> Account ID to Use</param>
 /// <param name="dto"> Dto for withdraw</param>
 /// <remarks>
 /// Balance cannot be negative
 /// </remarks>
 /// <response code="200">Returns status code</response>
 [HttpPatch("{accountId:int}/transfer", Name = "TransferBetweenAccounts")]
 [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
 [ProducesResponseType(StatusCodes.Status200OK)]
 [SwaggerRequestExample(typeof(TransferRequestDto), typeof(TransferRequestExamples))]
 ```
Configs are here: ```src/WebApi/Common/Extensions/SwaggerServices/SwaggerServiceExtension.cs```<br><br>

## Future Releases
- Authnetication and Authorization using Identity
- [Versioning API](https://www.infoworld.com/article/3562355/how-to-use-api-versioning-in-aspnet-core.html)
- Static analysis code using [SonarQube](https://docs.sonarqube.org/latest/analysis/scan/sonarscanner-for-msbuild/)
