namespace MachinesWorker.Models
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public ICollection<Parameter> Parameters { get; set; }
    }
}