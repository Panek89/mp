# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Machines.Api/MP.MachinesApi.csproj", "Machines.Api/"]
COPY ["Machines.DataAccess.EfCore/Machines.DataAccess.EfCore.csproj", "Machines.DataAccess.EfCore/"]
COPY ["Machines.Domain/Machines.Domain.csproj", "Machines.Domain/"]
RUN dotnet restore "Machines.Api/MP.MachinesApi.csproj"
COPY . .
WORKDIR "/src/Machines.Api"
RUN dotnet build "MP.MachinesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MP.MachinesApi.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MP.MachinesApi.dll"]
