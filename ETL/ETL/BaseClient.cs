using System.Text;
using RabbitMQ.Client;

namespace RabbitMqDll;

public class BaseClient
{
    protected static BaseClient? _instance;
    protected IModel _channel;

    protected BaseClient()
    {
        if (Environment.GetEnvironmentVariable("RABBIT_HOST") == string.Empty)
            throw new Exception("HOST is empty");
        string host = Environment.GetEnvironmentVariable("RABBIT_HOST");
        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            UserName = "guest",
            Password = "guest",
            HostName = host
        };
        IConnection connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }
 
    public static BaseClient Start()
    {
        if (_instance == null)
            _instance = new BaseClient();
        return _instance;
    }
}