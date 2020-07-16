#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Pulsar.Customers.Api/Pulsar.Customers.Api.csproj", "Pulsar.Customers.Api/"]
RUN dotnet restore "Pulsar.Customers.Api/Pulsar.Customers.Api.csproj"
COPY . .
WORKDIR "/src/Pulsar.Customers.Api"
RUN dotnet build "Pulsar.Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pulsar.Customers.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pulsar.Customers.Api.dll"]