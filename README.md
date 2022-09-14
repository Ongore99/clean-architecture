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
Dependents on **Application, Domain and Infrastructure Projects.**

Consists of *Common* and *Endpoints* Folders. <br><br> 
- **Common folder** contains all Common things related our API (For example you may create folderw for Extensions, Bases, Attributes, Helpers, Filters, Middlewares). <br><br>
- **Endpoints folder** contains Controllers, Request Dtos with their Validaitons, Swagger Examples  (Divided based on entity that you are mainly working on) <br><br>

Contains Swagger, Request DTOs for binding, [Swagger Examples](https://medium.com/@niteshsinghal85/multiple-request-response-examples-for-swagger-ui-in-asp-net-core-864c0bdc6619), Library Configurations, Sending Commands and Queries using [Mediatr](https://www.youtube.com/watch?v=YzOBrVlthMk). <br><br>
 - **Library Configurations Path:** ```src/WebApi/Common/Extensions``` (Ef, Validation, Mediatr setup are here)<br><br>
 - **Validation of Request Dtos:** ```src/WebApi/Endpoints/Accounts/Dtos/Requests``` (Divided based on entity). Dtos contain Validation 
logic using [Fluent Validation Library](https://fluentvalidation.net/). Fluent Validation [integrated with Swagger](https://anexinet.com/blog/asp-net-core-fluentvalidation-swagger/)  to show what validation the Dto has. See example of validation in src/WebApi/Endpoints/Accounts/Dtos/Requests/TransferRequestDto.cs <br><br>
 - **Swagger Response and Request Examples:** ```src/WebApi/Endpoints/Accounts/Dtos/SwaggeExamples``` (Divided based on entity) <br><br>
