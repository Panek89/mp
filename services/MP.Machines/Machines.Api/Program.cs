using Machines.DataAccess.EfCore;
using Machines.DataAccess.EfCore.Repositories;
using Machines.DataAccess.EfCore.UnitOfWork;
using Machines.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SQLDEVELOPER2016"),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

# region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IParameterRepository, ParameterRepository>();
builder.Services.AddTransient<IMachineRepository, MachineRepository>();
# endregion

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

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
