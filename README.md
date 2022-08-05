# .NET Core Async WebAPI
[![Docker Image CI](https://github.com/abhishekgoenka/AspDotnetCoreAsyncWebAPI/actions/workflows/ci-docker.yml/badge.svg)](https://github.com/abhishekgoenka/AspDotnetCoreAsyncWebAPI/actions/workflows/ci-docker.yml)
[![Build Status](https://dev.azure.com/abhishekgoenkapublic/github-projects/_apis/build/status/AspDotnetCoreAsyncWebAPI-main-ci?branchName=master)](https://dev.azure.com/abhishekgoenkapublic/github-projects/_build/latest?definitionId=8&branchName=master)

This project demonstrates basic CRUD Web API operations using Async pattern in .NET 5. This demo application uses Entity Framework Core's async API to persist data. The client is built in Angular framework using material theme.

The project publishes docker images for web-api and angular client. The project uses serilog(seq) for logging infrastructure.


# Before you begin
Download and install [.NET SDK](https://go.microsoft.com/fwlink/?LinkID=660852&clcid=0x409).

# Run Application
Open DotNetCoreAsysnSample solution in Visual Studio 2019 or higher. Build and run the solution. It would seed the data on first use. Run `server.bat`

# Test APIs
> https://localhost:8080/api/Seed/Import

> https://localhost:8080/api/v1/customers

> https://localhost:8080/api/v1/customers/5

> https://localhost:8080/api/v1/customers/page/2/20

# Swagger
Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models.
> https://localhost:8080/swagger

# Build and Run Docker Images
> docker-compose up --build

## Seed database
Open swagger URL in browser `http://localhost:8080/swagger/index.html` and execute `/api/Seed/Import` API.

### Seq URL
> http://localhost:8005

### .NET API
> http://localhost:8080/swagger/

### Angular Client
> http://localhost/
