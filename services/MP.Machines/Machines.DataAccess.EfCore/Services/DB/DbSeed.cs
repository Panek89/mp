using Bogus;
using Machines.Domain.Configuration.Options;
using Machines.Domain.Constant;
using Machines.Domain.Helpers;
using Machines.Domain.Interfaces;
using Machines.EventServiceBus.Services.Machines;
using Microsoft.Extensions.Options;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore.Services.DB
{
    public class DbSeed : IDbSeed
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly SeedOptions _seedOptions;
        private readonly IMachineSender _machineSender;

        public DbSeed(IUnitOfWork unitOfWork, IOptions<SeedOptions> seedOptions, IMachineSender machineSender)
        {
            _unitOfWork = unitOfWork;
            _seedOptions = seedOptions.Value;
            _machineSender = machineSender;
        }

        public async Task<int> Seed()
        {
            var machines = await _unitOfWork.Machines.GetAllAsync();
            if (machines.Count() > 0 && !_seedOptions.DoSeedWhenDataExists) 
            {
                return 0;
            }

            var generatedParameters = GenerateParameters(_seedOptions.ParametersCount);
            var generatedMachines = GenerateMachines(_seedOptions.MachinesCount, generatedParameters);

            _machineSender.SendMachines(ServiceBusMachineHelper.MapMultipleServiceBusMachineDTOs(generatedMachines, Domain.Enums.ServiceBusEnumStatus.Create));
            await _unitOfWork.Parameters.AddRangeAsync(generatedParameters);
            await _unitOfWork.Machines.AddRangeAsync(generatedMachines);
            
            var result = await _unitOfWork.CompleteAsync();
            return result;
        }

        public IList<Parameter> GenerateParameters(int count)
        {
            var parameters = new Faker<Parameter>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Key, f => f.Random.ListItem<string>(ParameterKeysList.keys))
                .RuleFor(p => p.MinValue, f => f.Random.Double(0, 10))
                .RuleFor(p => p.MaxValue, f => f.Random.Double(11, 20));
            
            return parameters.Generate(count);
        }

        public IList<Machine> GenerateMachines(int count, IList<Parameter> parameters)
        {
            var machines = new Faker<Machine>()
                .RuleFor(m => m.Id, f => Guid.NewGuid())
                .RuleFor(m => m.Manufacturer, f => f.Random.ListItem<string>(MachineManufacturersList.manufacturers))
                .RuleFor(m => m.Model, f => f.Random.ListItem<string>(MachineModelsList.models))
                .RuleFor(m => m.Parameters, f => f.Random.ListItems<Parameter>(parameters, _seedOptions.MachineParametersCount));

            return machines.Generate(count);
        }
    }
}