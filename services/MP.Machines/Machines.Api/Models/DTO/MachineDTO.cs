using MP.MachinesApi.Models;

namespace Machines.Api.Models.DTO
{
    public class MachineDTO
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public ICollection<ParameterBaseDTO> Parameters { get; set; }
    }
}