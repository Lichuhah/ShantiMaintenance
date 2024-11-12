using System.Text;
using RabbitMQ.Client;

namespace RabbitMqDll;

public class Sender : BaseClient 
{
    public static Sender Start()
    {
        if (_instance == null)
            _instance = new Sender();
        return _instance as Sender;
    }
    public void SendMessage(string message)
    {
        byte[] messagebuffer = Encoding.Default.GetBytes(message);
        try
        {
            _channel.BasicPublishAsync("master", "master", messagebuffer).GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Success sended message");
    }
}