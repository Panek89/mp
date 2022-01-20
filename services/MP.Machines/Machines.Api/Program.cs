using Machines.Api.Extensions;
using Machines.DataAccess.EfCore;
using Machines.DataAccess.EfCore.Services.DB;
using Machines.Domain.Configuration.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SQLDEVELOPER2016"),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

# region Options
builder.Services.Configure<SeedOptions>
    (builder.Configuration.GetSection(SeedOptions.Seed));
# endregion

# region Extensions
builder.Services.RepoServiceExtensions();
builder.Services.DbServiceExtensions();
builder.Services.ValidatorsServiceExtensions();
# endregion

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var appContext = services.GetRequiredService<IDbInitialize>();
    var dbExists = appContext.Initialize();

    if (dbExists)
    {
        var dbSeed = services.GetRequiredService<IDbSeed>();
        var seedOptions = services.GetRequiredService<IOptions<SeedOptions>>();
        if (seedOptions.Value.DoSeed)
        {
            await dbSeed.Seed();
        }
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
