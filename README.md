# Architecture
The Projects tries to follow clean architecture with CQRS pattern. CQRS is done using Mediatr library. Here is digram of architecture:
![Clean-Architecture-Diagram2](https://user-images.githubusercontent.com/31799470/189880565-38ff27a4-fbb2-4e4c-8d8b-b5c108c29a05.png)

The **WebApi Project** is  **Infrastructure** is dependent on **Domain and Application** projects. **Application** is dependent on Domain Project.
## Usefull links 
https://www.youtube.com/watch?v=tLk4pZZtiDY&t=379s
# Solution Structure
## Conventions
All folders should be created using plural form.<br>
```diff
+ Correct: Extensions
- Incorrect: Extension
```
## Responsiblity of each layer
### **WebApi Project** 
Consists of *Common* and *Endpoints* Folders. Common folder contains all Common things related our API (For example you may create folderw for Extensions, Bases, Attributes, Helpers, Filters, Middlewares)<br><br>
Dependents on **Application, Domain and Infrastructure Projects.**
<br><br>Contains Swagger, Request DTOs for binding, Swagger Examples, Library Configurations, Sending Commands and Queries using Mediatr. <br><br>
 - **Library Configurations Path:** ```src/WebApi/Common/Extensions``` (Ef, Validation, Mediatr setup are here)<br><br>
 - **Request DTOs for binding and their Validations:** ```src/WebApi/Endpoints/Accounts/Dtos/Requests``` (Divided based on entity). Dtos contain Validation 
logic using [Fluent Validation Library](https://fluentvalidation.net/). See example of validation in src/WebApi/Endpoints/Accounts/Dtos/Requests/TransferRequestDto.cs <br><br>
 - **Swagger Response and Request Examples:** ```src/WebApi/Endpoints/Accounts/Dtos/SwaggeExamples``` (Divided based on entity) <br><br>
