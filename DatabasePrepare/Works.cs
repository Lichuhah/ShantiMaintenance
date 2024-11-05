using Npgsql;

namespace DatabasePrepare;

public static class Works
{
    static string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
    
    public static void Clear()
    {
        Postgres.Truncate("work_status");
        Postgres.Truncate("work");
    }

    public static void Init()
    {
        using (var reader = new StreamReader(path + "PdM_maint.csv"))
        {
            int id = 1;
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                DateTime time = DateTime.Parse(values[0]);
                time = time.AddYears(9);
                if(time > DateTime.UtcNow) continue;
                DateTime.SpecifyKind(time, DateTimeKind.Utc);
                int assetId = Int32.Parse(values[1]);
            
                using (var cmd = new NpgsqlCommand("insert into public.work (register_date, name, asset_id, type) values (@d, @n, @a, 1);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("n", "Обслуживание турбины " + assetId + " от " + time.ToShortDateString()); 
                    cmd.Parameters.AddWithValue("a", assetId);
                    Postgres.ExecCommand(cmd);
                }
            
                using (var cmd = new NpgsqlCommand("insert into work_status (status, start_date, work_id) values (3, @d, @i);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("i", id);
                    Postgres.ExecCommand(cmd);
                }
                
                id++;
            }
        }
    }
}