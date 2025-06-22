using System.Text;
using System.Text.Json;
using ManagementApi.Entities;
using ManagementApi.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ManagementApi.Messaging;

public class RabbitMqPublisher : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string ExchangeName = "user_exchange";

    public RabbitMqPublisher(IOptions<RabbitMqOptions> options)
    {
        var config = options.Value;
        var factory = new ConnectionFactory()
        {
            HostName = config.HostName,
            Port = config.Port,
            UserName = config.UserName,
            Password = config.Password
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);
    }

    public void PublishUserCreated(User user)
    {
        var message = JsonSerializer.Serialize(user);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: null, body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}