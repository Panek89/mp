# mp

Manufacturing Platform (MP)

## Run migrations and update DB in project

Create Initial migrations

```commandline
dotnet ef --startup-project .\services\MP.Machines\Machines.Api\ migrations add InitialCreate --project .\services\MP.Machines\Machines.DataAccess.EfCore\
```

Update database

```commandline
dotnet ef --startup-project .\services\MP.Machines\Machines.Api\ database update --project .\services\MP.Machines\Machines.DataAccess.EfCore\
```
