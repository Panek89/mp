using Machines.Api.Extensions;
using Machines.Api.Startup;
using Machines.DataAccess.EfCore;
using Machines.Domain.Configuration.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MSSQL"),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

# region Options
builder.Services.Configure<SeedOptions>
    (builder.Configuration.GetSection(SeedOptions.Seed));
builder.Services.Configure<RabbitMqOptions>
    (builder.Configuration.GetSection(RabbitMqOptions.RabbitMQ));
# endregion

# region Extensions
builder.Services.RepoServiceExtensions();
builder.Services.DbServiceExtensions();
builder.Services.ValidatorsServiceExtensions();
builder.Services.EventServiceBusExtensions();
# endregion

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    await AppStartup.ApplicationStartup(serviceScope);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Move this section out, if want have Swagger also in Docker ENV
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
