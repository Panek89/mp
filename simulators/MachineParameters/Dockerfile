# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore "MachineParameters.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MachineParameters.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MachineParameters.csproj" -c Release -o /app/publish

# Build runtime image
FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MachineParameters.dll"]