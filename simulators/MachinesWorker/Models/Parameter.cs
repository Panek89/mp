namespace MachinesWorker.Models
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
