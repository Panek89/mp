using Machines.Domain.ServiceBus;

namespace Machines.EventServiceBus.Services.Machines
{
    public interface IMachineSender
    {
        void SendMachine(ServiceBusMachineDTO machine);
        void SendMachines(IEnumerable<ServiceBusMachineDTO> machines);
    }
}