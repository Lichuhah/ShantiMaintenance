using Confluent.Kafka;

namespace ETL;

public class KafkaDll
{
    public static void Send(string data)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092" // Update with your Kafka broker address
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            string topic = "my-topic";
            string message = data;
            
            try
            {
                var deliveryResult = producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();
                Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error producing message: {e.Error.Reason}");
            }
        }
    }

    public static void Read()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", // Update with your Kafka broker address
            GroupId = "my-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
        {
            consumer.Subscribe("my-topic");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                var consumeResult = consumer.Consume(cts.Token);
                Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Closing consumer.");
                consumer.Close();
            }
        }
    }
}