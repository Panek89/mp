namespace  Machines.Domain.Configuration.Options
{
    public class SeedOptions
    {
        public const string Seed = "Seed";

        public bool DoSeed { get; set; }
        public int MachinesCount { get; set; }
        public int ParametersCount { get; set; }
        public int MachineParametersCount { get; set; }
    }
}