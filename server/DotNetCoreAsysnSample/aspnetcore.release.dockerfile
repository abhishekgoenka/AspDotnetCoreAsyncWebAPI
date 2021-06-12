FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

# Copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR "/src"
RUN dotnet build "DotNetCoreAsysnSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCoreAsysnSample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetCoreAsysnSample.dll"]
