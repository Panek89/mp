using FluentValidation;
using FluentValidation.AspNetCore;
using Machines.Api.Validators;
using Machines.DataAccess.EfCore.Repositories;
using Machines.DataAccess.EfCore.Services.DB;
using Machines.DataAccess.EfCore.UnitOfWork;
using Machines.Domain.Interfaces;
using Machines.EventServiceBus.Services.Machines;
using Machines.EventServiceBus.Services.RabbitMQ;
using MP.MachinesApi.Models;

namespace Machines.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RepoServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IParameterRepository, ParameterRepository>();
            services.AddTransient<IMachineRepository, MachineRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection DbServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IDbInitialize, DbInitialize>();
            services.AddScoped<IDbSeed, DbSeed>();

            return services;
        }

        public static IServiceCollection ValidatorsServiceExtensions(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<Parameter>, ParameterValidator>();
            services.AddTransient<IValidator<Machine>, MachineValidator>();

            return services;
        }

        public static IServiceCollection EventServiceBusExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddTransient<IMachineSender, MachineSender>();

            return services;
        }
    }
}