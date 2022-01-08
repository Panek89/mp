namespace Machines.Api.Models.DTO
{
    public class ParameterBaseDTO
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}