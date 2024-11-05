using Cassandra;

namespace DatabasePrepare;

public static class Telemetry
{
    public static void Clear()
    {
        string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
        Cluster cluster = Cluster.Builder()
            .AddContactPoints("localhost")
            .WithPort(9042)
            .WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy("datacenter1"))
            .WithAuthProvider(new PlainTextAuthProvider("cassandra", "cassandra"))
            .Build();
        ISession _session = cluster.Connect();
        _session.ChangeKeyspace("master");
        for (int i = 0; i < 101; i++)
        {
            string cmd =
                $"truncate table sensor_data_{i});";
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

    public static void Init()
    {
        string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
        int i = 0;
        Cluster cluster = Cluster.Builder()
            .AddContactPoints("localhost")
            .WithPort(9042)
            .WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy("datacenter1"))
            .WithAuthProvider(new PlainTextAuthProvider("cassandra", "cassandra"))
            .Build();
        ISession _session = cluster.Connect();
        _session.ChangeKeyspace("master");
        using (var reader = new StreamReader(path + "PdM_telemetry.csv"))
        {
            List<string> listA = new List<string>();
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                Console.WriteLine(i);
                var line = reader.ReadLine();
                var values = line.Split(',');
                DateTime time = DateTime.Parse(values[0]);
                time = time.AddYears(9);
                string strTime = time.ToString("o");
                int assetId = Int32.Parse(values[1]);
                string cmd = "create table if not exists master.sensor_data_" + assetId +
                             "(key text primary key, datetime timestamp, assetId int, volt double, rotate double, pressure double, vibration double);";
                try
                {
                    _session.Execute(cmd);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                cmd =
                    $"insert into sensor_data_{assetId} (\"key\", assetid, datetime, pressure, rotate, vibration, volt) values (\'{strTime}\',{assetId},\'{strTime}\',{values[2]},{values[3]},{values[4]},{values[5]});";
                try
                {
                    _session.Execute(cmd);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                i++;
            }
        }
    }
}