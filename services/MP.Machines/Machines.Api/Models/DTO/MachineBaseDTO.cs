namespace Machines.Api.Models.DTO
{
    public class MachineBaseDTO
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}