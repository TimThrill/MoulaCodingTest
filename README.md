# Third Party Libraries
- Swashbuckle.AspNetCore
- FluentAPI
- AutoMapper
- MediatR

# How To Use
- Go to Moula.Payment.GateWay folder via using Package Manager Console and run following command to apply migration
> dotnet ef database update
- Start the application and you will be navigated to the Swagger page

# Design Philosophy
In this solution, I try to use following pattern:
- Repository & Unit of Work
- CQRS
- Dependency Injection
- TDD

The structure of the code mainly referenced to Microsoft guideline, which is an open source repo called eShop

# Future improvement
With current design, the code base can be easily adapted to a Microservice product by adding domain events to relevant objects
and integrate a message broker such as Azure Service Bus.

# About CI configuration
There is a yaml file which is designed to be used in Azure DevOps. 
It will build and publish the project and also generate sql migration scripts.
However, it has been verified for this project so please do not be surprised if it does not work.

In this project, the only configuration difference between UAT and Prod would be the SQL connection string,
which can be injected in Azure DevOps release pipeline or configure at Azure App Service manually.
