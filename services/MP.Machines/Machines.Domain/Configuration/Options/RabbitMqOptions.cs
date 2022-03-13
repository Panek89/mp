namespace Machines.Domain.Configuration.Options
{
    public class RabbitMqOptions
    {
        public const string RabbitMQ = "RabbitMQ";
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string[] QueuesNames { get; set; }
    }

    public class QueueOptions
    {
        public string QueueName { get; set; }
    }
}