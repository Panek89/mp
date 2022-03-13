using Machines.Domain.Constant;
using Machines.Domain.Enums;
using MP.MachinesApi.Models;

namespace Machines.Domain.ServiceBus
{
    public class ServiceBusMachineDTO : Machine
    {
        public ServiceBusEnumStatus ServiceBusStatus { get; set; }
        public ServiceBusMachineDTO(Machine machine, ServiceBusEnumStatus status)
        {
            Id = machine.Id;
            Manufacturer = machine.Manufacturer;
            Model = machine.Model;
            Parameters = machine.Parameters;
            ServiceBusStatus = status;
        }
    }
}