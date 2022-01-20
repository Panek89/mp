namespace Machines.Api.Models.DTO
{
    public class ParameterDTO
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public ICollection<MachineBaseDTO> Machines { get; set; }
    }
}