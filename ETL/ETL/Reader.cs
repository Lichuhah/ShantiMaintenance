using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqDll;

public class Reader : BaseClient
{
    public static Reader Start()
    {
        if (_instance == null)
            _instance = new Reader();
        return _instance as Reader;
    }
    
    public void StartReading(EventHandler<BasicDeliverEventArgs> handler)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += handler;
        string result = _channel.BasicConsume(queue: "master",
            autoAck: true,
            consumer: consumer);
        Console.WriteLine($"Result: {result}");
    }
}