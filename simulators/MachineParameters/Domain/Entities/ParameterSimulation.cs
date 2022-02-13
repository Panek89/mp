namespace MachineParameters.Domain.Entities
{
    public class ParameterSimulation
    {
        public Guid Id { get; set; }
        public Guid MachineId { get; set; }
        public Guid ParameterId { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }  
    }
}