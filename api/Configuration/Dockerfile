FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 81

ENV ASPNETCORE_URLS=http://+:81

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["Configuration.Application/Configuration.Application.csproj", "Application/"]
COPY ["Configuration.Infrastructure/Configuration.Infrastructure.csproj", "Infrastructure/"]
COPY ["Configuration.Domain/Configuration.Domain.csproj", "Domain/"]
COPY ["Configuration.Api/Configuration.Api.csproj", "Api/"]
COPY ["Configuration.Tests/Configuration.Tests.csproj", "Tests/"]

RUN pwd
RUN dotnet restore "Api/Configuration.Api.csproj"

COPY . .
WORKDIR "/src/Configuration.Api"
RUN dotnet build "Configuration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Configuration.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Configuration.Api.dll"]