using System.Text;
using RabbitMQ.Client;

namespace RabbitMqDll;

public class BaseClient
{
    protected static BaseClient? _instance;
    protected IChannel _channel;

    protected BaseClient()
    {
        if (Environment.GetEnvironmentVariable("HOST") == string.Empty)
            throw new Exception("HOST is empty");
        string host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
        Console.WriteLine("HOST: " + Environment.GetEnvironmentVariable("HOST"));
        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            UserName = "guest",
            Password = "guest",
            HostName = host
        };
        IConnection connection = null;
        while (connection?.IsOpen != true)
        {
            try
            {
                connection = connectionFactory.CreateConnectionAsync().Result;
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
        }
        _channel = connection.CreateChannelAsync().Result;
    }
 
    public static BaseClient Start()
    {
        if (_instance == null)
            _instance = new BaseClient();
        return _instance;
    }
}