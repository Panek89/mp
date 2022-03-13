namespace Machines.EventServiceBus.Services.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        bool CreateConnection(string hostName, string userName, string password);
        bool CheckIfConnectionExists();
        bool CreateQueue(string queueName);
        void SendDataOnQueue(string queueName, byte[] body);
    }
}