# .NET Core Async WebAPI
[![Build Status](https://dev.azure.com/abhishekgoenkapublic/github-projects/_apis/build/status/AspDotnetCoreAsyncWebAPI-main-ci?branchName=master)](https://dev.azure.com/abhishekgoenkapublic/github-projects/_build/latest?definitionId=8&branchName=master)


This project demonstrates basic CRUD Web API operations using Async pattern in .NET 5. This demo application uses Entity Framework Core's async API to persist data.

# Before you begin
Download and install [.NET SDK](https://go.microsoft.com/fwlink/?LinkID=660852&clcid=0x409).

# Run Application
Open DotNetCoreAsysnSample solution in Visual Studio 2019 or higher. Build and run the solution. It would seed the data on first use.

# Test APIs
> http://localhost:55959/api/v1/customers

> http://localhost:55959/api/v1/customers/5

> http://localhost:55959/api/v1/customers/page/2/20

# Swagger
Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models.
> http://localhost:55959/swagger

# Docker Build
> docker build -f aspnetcore.release.dockerfile -t abhishek1950/aspnetcore .

# Run the Docker image
> docker run -it --rm -p 8000:80 --name dotnet_core_asysn_sample abhishek1950/aspnetcore


You must navigate to the container IP (as opposed to http://localhost) in your browser directly when using Windows containers. You can get the IP address of your container with the following steps:

1. Open up another command prompt.
1. Run `docker ps` to see your running containers. The "dotnet_core_asysn_sample" container should be there.
1. Run `docker exec dotnet_core_asysn_sample ipconfig`.
1. Copy the container IP address and paste into your browser (for example, `172.29.245.43`).

```console
C:\code\AspDotnetCoreAsyncWebAPI\DotNetCoreAsysnSample>docker exec dotnet_core_asysn_sample ipconfig

Windows IP Configuration


Ethernet adapter Ethernet:

   Connection-specific DNS Suffix  . : contoso.com
   Link-local IPv6 Address . . . . . : fe80::1967:6598:124:cfa3%4
   IPv4 Address. . . . . . . . . . . : 172.29.245.43
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . : 172.29.240.1
```

Note: [`docker exec`](https://docs.docker.com/engine/reference/commandline/exec/) supports identifying containers with name or hash. The name is used above. It runs a new command (as opposed to the [entrypoint](https://docs.docker.com/engine/reference/builder/#entrypoint)) in a running container.
