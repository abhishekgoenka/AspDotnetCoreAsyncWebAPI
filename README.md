# .NET Core Async WebAPI
This project demonstrates basic CRUD Web API operations using Async pattern in .NET Core 2.0. This demo application uses Entity Framework Core's async API to persist data.

# Before you begin
Download and install [.NET Core SDK](https://go.microsoft.com/fwlink/?LinkID=660852&clcid=0x409).

# Run Application
Open DotNetCoreAsysnSample solution in Visual Studio 2017 or higher. Build and run the solution. It would seed the data on first use.

# Test APIs
> http://localhost:55959/api/customers

> http://localhost:55959/api/customers/5

> http://localhost:55959/api/customers/page/2/20

# Swagger
Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models.
> http://localhost:55959/swagger

# Docker Build
> docker build -f aspnetcore.release.dockerfile -t abhishek1950/aspnetcore .

# Run the Docker image
> docker run -d -p 8080:80 --name myapp abhishek1950/aspnetcore
