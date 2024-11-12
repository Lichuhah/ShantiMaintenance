using Cassandra;
using ETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ISession = Cassandra.ISession;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CassandraDll;

public class Repository
{
    private static Repository? _instance;
    private ISession _session;
    public static Repository Connect()
    {
        if (_instance == null)
            _instance = new Repository();
        return _instance;
    }

    private Repository()
    {
        Console.WriteLine($"Cassandra host: {Environment.GetEnvironmentVariable("CASSANDRA_HOST")}");
        Cluster cluster = Cluster.Builder()
            .AddContactPoints(Environment.GetEnvironmentVariable("CASSANDRA_HOST"))
            .WithPort(9042)
                .WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy("datacenter1"))
                .WithAuthProvider(new PlainTextAuthProvider("cassandra", "cassandra"))
            .Build();
        _session = cluster.Connect();
        Console.WriteLine("Connected to cluster: " + cluster.Metadata.ClusterName);
        var keyspaceNames = _session
            .Execute("SELECT * FROM system_schema.keyspaces")
            .Select(row => row.GetValue<string>("keyspace_name"));
        Console.WriteLine("Found keyspaces:");
        foreach (var name in keyspaceNames)
        {
            Console.WriteLine("- {0}", name);
        }
        _session.ChangeKeyspace("master");
    }

    public void CheckTable(int id, JObject row)
    {
        string cmd = "create table if not exists master.sensor_data_" + id + "(key text primary key, datetime timestamp,";

        foreach (var p in row.Properties())
        {
            if(p.Name == "key") continue;
            
            cmd += p.Name + " ";
            Console.WriteLine(row[p.Name].Type);
            switch (row[p.Name].Type)
            {
                case JTokenType.Date: cmd += "timestamp,"; break;
                case JTokenType.Float: cmd += "double,"; break;
                default: cmd += "text,"; break;
            }
        }

        cmd = cmd.Remove(cmd.Length - 1);
        cmd += ");";
        
        Console.WriteLine("CREATE TABLE master.sensor_data_" + id);
        Console.WriteLine(cmd);
        try
        {
            _session.Execute(cmd);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddRow(JObject? row)
    {
        if(row == null) return;
        if (row["timestamp"] == null || row["key"] == null)
        {
            Console.WriteLine("Row not contain timestamp or key");
            return;
        }

        List<string> properties = row.Properties().Select(x=>x.Name).ToList();
        CheckTable(Convert.ToInt32(RedisDll.GetAssetId(row["key"].ToString())), row);
        
        //TODO: get id from session
        string cmd = "insert into sensor_data_" + RedisDll.GetAssetId(row["key"].ToString()) + "(";
        cmd += "key,datetime,";
        foreach (var p in properties)
        {
            if(p == "key") continue;
            cmd += p + ",";
        }
        cmd = cmd.Remove(cmd.Length - 1);
        cmd += ") values (";
        cmd += $"'{row["key"].ToString() + DateTime.Parse(row["timestamp"].ToString()).ToOADate()}',";
        cmd += $"'{DateTime.Parse(row["timestamp"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}',";

        foreach (var p in properties)
        {
            if(p == "key") continue;
            if(row[p].Type == JTokenType.Float)
                cmd += $"{row[p]},";
            else cmd += $"'{row[p]}',";
        }
        cmd = cmd.Remove(cmd.Length - 1);
        cmd += ");";
        Console.WriteLine(cmd);
        try
        {
            _session.Execute(cmd);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}