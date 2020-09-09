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
