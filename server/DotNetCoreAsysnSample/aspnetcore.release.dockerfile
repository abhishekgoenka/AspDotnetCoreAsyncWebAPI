FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

MAINTAINER Abhishek Goenka

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Set Environment Variables
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
# ENV ASPNETCORE_URLS=http://*:5000

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DotNetCoreAsysnSample.dll"]


# FROM microsoft/aspnetcore

# MAINTAINER Abhishek Goenka

# COPY . /var/www
# WORKDIR /var/www

# # VOLUME [ "/var/www" ]

# ENV DOTNET_USE_POLLING_FILE_WATCHER=1
# ENV ASPNETCORE_URLS=http://*:5000

# # WORKDIR /var/www/aspnetcoreapp

# CMD ["/bin/bash", "-c", "dotnet restore && dotnet run"]