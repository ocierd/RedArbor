# Solution creation and project setup commands for RedArbor
dotnet new sln --name RedArbor

# WebApi project
dotnet new webapi --name RedArbor.WebApi --output WebApi  --use-controllers --use-program-main
dotnet sln add WebApi
dotnet add WebApi package Swashbuckle.AspNetCore

# Domain project
dotnet new classlib --name RedArbor.Domain --output Domain
dotnet sln add Domain
dotnet add Domain package MediatR


# Application project
dotnet new classlib --name RedArbor.Application --output Application
dotnet sln add Application
# Application project packages
dotnet add Application package Microsoft.EntityFrameworkCore
dotnet add Application package MediatR
# Application project reference
dotnet add Application reference Domain


# Infrastructure project
dotnet new classlib --name RedArbor.Infrastructure --output Infrastructure
dotnet sln add Infrastructure
# Infrastructure project packages
dotnet add Infrastructure package Microsoft.Extensions.DependencyInjection
dotnet add Infrastructure package Microsoft.EntityFrameworkCore
dotnet add Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
# Infrastructure project reference
dotnet add Infrastructure reference Application
dotnet add Infrastructure package Dapper


# WebApi project references
dotnet add WebApi reference Application
dotnet add WebApi reference Infrastructure