using MachineParameters.DataAccess;
using MachineParameters.Services;
using MachineParameters.Services.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MSSQL"),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

builder.Services.AddHostedService<SendParametersService>();
builder.Services.AddScoped<IDbInitialize, DbInitialize>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var appContext = services.GetRequiredService<IDbInitialize>();
    var dbExists = appContext.Initialize();
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
