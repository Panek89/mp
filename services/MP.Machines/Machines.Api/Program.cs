// using System.Text.Json.Serialization;
using Machines.DataAccess.EfCore;
using Machines.DataAccess.EfCore.Repositories;
using Machines.DataAccess.EfCore.Services.DB;
using Machines.DataAccess.EfCore.UnitOfWork;
using Machines.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// .AddJsonOptions(options => 
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(""),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

# region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IParameterRepository, ParameterRepository>();
builder.Services.AddTransient<IMachineRepository, MachineRepository>();
# endregion

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitialize, DbInitialize>();
builder.Services.AddScoped<IDbSeed, DbSeed>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var appContext = services.GetRequiredService<IDbInitialize>();
    var dbExists = appContext.Initialize();

    if (dbExists)
    {
        var dbSeed = services.GetRequiredService<IDbSeed>();
        await dbSeed.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
