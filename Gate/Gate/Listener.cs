namespace RabbitMqDll;

public abstract class Listener
{
    protected Sender sender = new Sender();

    public abstract void Start();
}