# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app


# Copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR "/src"
RUN dotnet build "DotNetCoreShared.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCoreShared.csproj" -c Release -o /src/assemblies
RUN rm "DotNetCoreShared.csproj"