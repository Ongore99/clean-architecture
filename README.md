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

Contains Swagger, Request DTOs for binding, [Swagger Examples](https://medium.com/@niteshsinghal85/multiple-request-response-examples-for-swagger-ui-in-asp-net-core-864c0bdc6619), Library Configurations, Sending Commands and Queries using [Mediatr](https://www.youtube.com/watch?v=YzOBrVlthMk). <br><br>
 - **Library Configurations Path:** ```src/WebApi/Common/Extensions``` (Ef, Validation, Mediatr and other services cnofigs are here)<br><br>
 - **Validation of Request Dtos:** ```src/WebApi/Endpoints/Accounts/Dtos/Requests``` (Divided based on entity). Dtos contain Validation 
logic using [Fluent Validation Library](https://fluentvalidation.net/). Fluent Validation [integrated with Swagger](https://anexinet.com/blog/asp-net-core-fluentvalidation-swagger/)  to show what validation the Dto has. See example of validation in src/WebApi/Endpoints/Accounts/Dtos/Requests/TransferRequestDto.cs <br><br>
 - **Swagger Response and Request Examples:** ```src/WebApi/Endpoints/Accounts/Dtos/SwaggeExamples``` (Divided based on entity) <br><br>

### **Infrastructure Project** 
Depends on **Application, Domain**.

Consists of *Common* and *Persistence* Folders + any folder for other tasks not related to Business logic. For example, Aunthecation Services, External API Calls, Cloud APIs, File Manipulation Services and others <br><br> 
- **Common folder** contains all Common things related our Infrasturcture (For example you may create folders for Extensions, Bases, Attributes, Helpers, Constants, Resources). <br><br>
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
- **Common folder** contains all Common things related our Application (For example you may create folders for Extensions, Bases, Interfaces, Helpers,Exceptions, Domain Validations, Contracts, Resources). <br><br>
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
### API - REST
