using System.Text;
using Machines.Domain.ServiceBus;
using Machines.EventServiceBus.Services.RabbitMQ;
using Newtonsoft.Json;

namespace Machines.EventServiceBus.Services.Machines
{
    public class MachineSender : IMachineSender
    {
        private readonly IRabbitMqConnection _connection;

        public MachineSender(IRabbitMqConnection connection)
        {
            _connection = connection;
        }
        public void SendMachine(ServiceBusMachineDTO machine)
        {
            if (_connection.CheckIfConnectionExists())
            {
                _connection.SendDataOnQueue("machines", ConvertObjectToBytes(machine));
            }
        }

        public void SendMachines(IEnumerable<ServiceBusMachineDTO> machines)
        {
            if (_connection.CheckIfConnectionExists())
            {
                foreach (var machine in machines)
                {
                    _connection.SendDataOnQueue("machines", ConvertObjectToBytes(machine));
                }
            }
        }

        private byte[] ConvertObjectToBytes(ServiceBusMachineDTO machine)
        {
            var json = JsonConvert.SerializeObject(machine);  
            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }
    }
}
