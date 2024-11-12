using System.Text;
namespace RabbitMqDll;
using MQTTnet;
using MQTTnet.Server;

public class MqttListener: Listener
{
    
    public override void Start()
    {
        var mqttServerOptions = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(55552)
            .Build();

        var mqttServer = new MqttFactory().CreateMqttServer(mqttServerOptions);
        mqttServer.InterceptingPublishAsync += async e =>
        {
            Console.WriteLine($"MQTT Message: {e.ApplicationMessage.Topic} - {e.ApplicationMessage.Payload}");
            string data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Sender sender = new Sender();
            sender.SendMessage(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        };

        mqttServer.StartAsync().GetAwaiter().GetResult();
    }
}