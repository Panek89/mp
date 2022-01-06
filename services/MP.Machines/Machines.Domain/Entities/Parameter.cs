namespace MP.MachinesApi.Models
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public ICollection<Machine> Machines { get; set; }
    }
}