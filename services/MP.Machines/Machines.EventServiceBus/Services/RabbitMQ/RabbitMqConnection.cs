using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Machines.EventServiceBus.Services.RabbitMQ
{

    public class RabbitMqConnection : IRabbitMqConnection
    {
        public IConnection _connection;
        private readonly ILogger<RabbitMqConnection> _logger;

        public RabbitMqConnection(ILogger<RabbitMqConnection> logger)
        {
            _logger = logger;
        }

        public bool CreateConnection(string hostName, string userName, string password)
        {
            var factory = new ConnectionFactory() { HostName = hostName, UserName = userName, Password = password, Port = 5672, VirtualHost = "/" };
            try
            {
                _connection = factory.CreateConnection();
                return true;
            }
            catch(BrokerUnreachableException exception)
            {
                _logger.LogError("Error on try create connection to RabbitMQ", exception);
                return false;
            }
        }

        public bool CheckIfConnectionExists()
        {
            bool connectionExists = _connection != null ? true : false;
            return connectionExists;
        }

        public bool CreateQueue(string queueName)
        {
            using(var channel = _connection.CreateModel())
            {
                try
                {
                    channel.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                    _logger.LogInformation($"Queue created successfully {queueName}");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error on create Queue: {queueName}, Exception: {ex}");
                    return false;
                }
            }
        }

        public void SendDataOnQueue(string queueName, byte[] body)
        {
            using(var channel = _connection.CreateModel())
            {
                channel.BasicPublish(exchange: "",
                                routingKey: queueName,
                                basicProperties: null,
                                body: body);
            }
        }
    }
}