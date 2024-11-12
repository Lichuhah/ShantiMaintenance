using System.Text;
using ETL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Task.Run(() =>
{
    try
    {
        RedisDll.Init();
    }
    catch (Exception e)
    {
        Thread.Sleep(10000);
        RedisDll.Init();
    }

    bool isStarted = false;
    while (!isStarted)
    {
        try
        {
            RabbitMqDll.Reader client = RabbitMqDll.Reader.Start();
            CassandraDll.Repository repository = CassandraDll.Repository.Connect();
            client.StartReading((model, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);
                repository.AddRow(Parser.Parse(msg));
                
                Console.WriteLine($" [x] Received {msg}");
            });
            isStarted = true;
            /*KafkaDll.Send("test");
            KafkaDll.Read();*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(10000);
            Console.WriteLine("Fail connect, retry 10 sec");
        }
    }
});

app.Run();



