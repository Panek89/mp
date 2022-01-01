namespace MP.MachinesApi.Models
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        ICollection<Parameter> Parameters { get; set; }
    }
}