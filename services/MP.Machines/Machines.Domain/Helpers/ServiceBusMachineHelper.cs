using Machines.Domain.Enums;
using Machines.Domain.ServiceBus;
using MP.MachinesApi.Models;

namespace Machines.Domain.Helpers
{
    public static class ServiceBusMachineHelper
    {
        public static ServiceBusMachineDTO MapSingleServiceBusMachineDTO(Machine machine, ServiceBusEnumStatus status)
        {
            ServiceBusMachineDTO serviceBusMachineDTO = new ServiceBusMachineDTO(machine, status);
            return serviceBusMachineDTO;
        } 

        public static List<ServiceBusMachineDTO> MapMultipleServiceBusMachineDTOs(IList<Machine> machines, ServiceBusEnumStatus status)
        {
            List<ServiceBusMachineDTO> serviceBusMachineDTOs = new List<ServiceBusMachineDTO>();
            foreach (var machine in machines)
            {
                serviceBusMachineDTOs.Add(new ServiceBusMachineDTO(machine, status));
            }
            
            return serviceBusMachineDTOs;
        }
    }
}