using StackExchange.Redis;

namespace ETL;

public class RedisDll
{
    public static void Init(){
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_HOST"));
        
        // Access the Redis database
        IDatabase db = redis.GetDatabase();

        // Sending data to Redis
        string key = "0";
        string value = "0";
        
        db.StringSetAsync(key, value).GetAwaiter().GetResult();
        Console.WriteLine($"Data sent to Redis: {key} = {value}");

        // Close the connection
        redis.Close();
    }

    public static string GetAssetId(string key)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_HOST"));
        IDatabase db = redis.GetDatabase();
        
        string retrievedValue = db.StringGetAsync(key).GetAwaiter().GetResult();

        // Close the connection
        redis.Close();
        return retrievedValue;
    }
}