# Manufacturing Platform

## Description

Portfolio project, for learning new technologies, don't use on production!
Project contains few microservices, which can you run on in premises environments or Docker.

Used technologies:

- .NET 6
- ASP.NET Core
- MSSQL 2019 DEV edition
- Docker

Main libraries:

- Entity Framework Core 6
- FluentValidation
- AutoMapper

## Microservices

### MP.Machines

Microservice responsible for managing machines and parameters in system.
It's contains three projects:

- API
- Data Access (configured with EF Core 6)
- Domain

#### Create and run migration

Run this command if there is no migrations in the project, or you want to refresh all migrations.
Create Initial migrations (run command in root of project)

```commandline
dotnet ef --startup-project .\services\MP.Machines\Machines.Api\ migrations add InitialCreate --project .\services\MP.Machines\Machines.DataAccess.EfCore\
```

Update database (run command in root of project)

```commandline
dotnet ef --startup-project .\services\MP.Machines\Machines.Api\ database update --project .\services\MP.Machines\Machines.DataAccess.EfCore\
```

#### MP.Machines - Configuration

The configuration file is at the path services\MP.Machines\Machines.Api\appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "MSSQL": "Server=mssqldb;Database=MP.Machines;User Id=sa;Password=MightyPassword2022!;"
  },
  "Seed" : {
    "DoSeed" : "true",
    "MachinesCount" : "5",
    "ParametersCount" : "20",
    "MachineParametersCount" : "3"
  },
  "AllowedHosts": "*"
}
```

What can be configured?

- Connection String for connection to DB
- Seed section, where can the database be seed preconfigured, whether it should take place and, if so, in what quantities it should be seed

By default it was filled and it works with that configuration (via Docker)

### MachineParameters

Microservice responsible for sending machine parameters data. Everything is placed at the moment in one ASP.NET Core Web Api project.

Access to data is provided by Entity Framework Core.

#### MachineParameters - Configuration

The configuration file is at the path simulators\MachineParameters\appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "SQL2019DEV": "Server=mssqldb;Database=MP.Sim.MachParams;User Id=sa;Password=MightyPassword2022!;"
  },
  "AllowedHosts": "*"
}
```

What can be configured?

- Connection String for connection to DB

By default it was filled and it works with that configuration (via Docker)

## Docker

There is also possible for run microservice via compose file,
it was automatically create MSSQL DB and run microservice's.
docker-compose.yml file was placed in root.

Command for create and run Docker containers:

```cmd
docker-compose up --build
```
