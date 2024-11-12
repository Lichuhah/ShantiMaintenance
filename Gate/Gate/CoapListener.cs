using System.Text;
using CoAP;
using CoAP.Server;
using CoAP.Server.Resources;

namespace RabbitMqDll;

public class CoapListener
{
    public void Start()
    {
        // Создание CoAP-сервера
        var server = new CoapServer(new CoapConfig()
        {
            DefaultPort = 55553,
            HttpPort = 55553,
        });
        server.Add(new BaseResource());
        server.Start();
    }
}

public class BaseResource : Resource
{
    public BaseResource() : base("base")
    {
    }

    protected override void DoPost(CoapExchange exchange)
    {
        string data = Encoding.UTF8.GetString(exchange.Request.Payload);
        Sender sender = new Sender();
        sender.SendMessage(data);
        exchange.Respond("Hello from C# CoAP Server!");
    }
}