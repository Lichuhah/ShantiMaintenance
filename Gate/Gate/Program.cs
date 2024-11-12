using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMqDll;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();


app.MapPost("/data", async e  =>
    {
        StreamReader reader = new StreamReader(e.Request.Body);
        string text = reader.ReadToEndAsync().GetAwaiter().GetResult();
        Sender sender = Sender.Start();
        sender.SendMessage(text);
        e.Response.StatusCode = StatusCodes.Status200OK;
        await e.Response.WriteAsJsonAsync(new { Message = "All todo items" });
    })
    .WithName("Data")
    .WithOpenApi();


MqttListener mqtt = new MqttListener();
CoapListener coap = new CoapListener();
mqtt.Start();
coap.Start();
app.Run();
