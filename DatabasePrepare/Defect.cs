using Npgsql;

namespace DatabasePrepare;

public static class Defect
{
    static string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
    public static void Clear()
    {
        Postgres.Truncate("defect_status");
        Postgres.Truncate("failure_status");
        Postgres.Truncate("failure");
        Postgres.Truncate("defect");
        Postgres.Truncate("failure_category");
        Postgres.Truncate("defect_category");
        
        for (int i = 1; i < 6; i++)
        {
            using (var cmd = new NpgsqlCommand("insert into failure_category (name) values (@name);"))
            {
                cmd.Parameters.AddWithValue("name", "Категория отказа " + i);
                Postgres.ExecCommand(cmd);
            }
            using (var cmd = new NpgsqlCommand("insert into defect_category (name) values (@name);"))
            {
                cmd.Parameters.AddWithValue("name", "Категория дефекта " + i);
                Postgres.ExecCommand(cmd);
            }
        }
    }

    public static void InitFailures()
    {
        using (var reader = new StreamReader(path + "PdM_failures.csv"))
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
                int catId = Int32.Parse(values[2][5].ToString());
            
                using (var cmd = new NpgsqlCommand("insert into public.failure (register_date, name, category_id) values (@d, @n, @c);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("n", "Отказ турбины " + assetId + " от " + time.ToShortDateString()); 
                    cmd.Parameters.AddWithValue("a", assetId);
                    cmd.Parameters.AddWithValue("c", catId);
                    Postgres.ExecCommand(cmd);
                }
            
                using (var cmd = new NpgsqlCommand("insert into failure_status (status, start_date, failure_id) values (3, @d, @i);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("i", id);
                    Postgres.ExecCommand(cmd);
                }
            
                id++;
            }
        }
    }

    public static void InitDefects()
    {
        using (var reader = new StreamReader(path + "PdM_errors.csv"))
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
                int catId = Int32.Parse(values[2][6].ToString());
                int fId = 0;
            
                using (var cmd = new NpgsqlCommand("select * from public.failure where register_date = @d"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    var dbReader = Postgres.GetReader(cmd);
                    if (dbReader != "")
                    {
                        fId = Convert.ToInt32(dbReader);
                    }
                }
                
                using (var cmd = new NpgsqlCommand("insert into public.defect (register_date, name, asset_id, category_id, failure_id) values (@d, @n, @a, @c, @f);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("n", "Дефект турбины " + assetId + " от " + time.ToShortDateString()); 
                    cmd.Parameters.AddWithValue("a", assetId);
                    cmd.Parameters.AddWithValue("c", catId);
                    cmd.Parameters.AddWithValue("f", fId > 0 ? fId : DBNull.Value);
                    Postgres.ExecCommand(cmd);
                }
            
                using (var cmd = new NpgsqlCommand("insert into defect_status (status, start_date, defect_id) values (3, @d, @i);"))
                {
                    cmd.Parameters.AddWithValue("d", time);
                    cmd.Parameters.AddWithValue("i", id);
                    Postgres.ExecCommand(cmd);
                }
            
                id++;
            }
        }
    }

    public static void Init()
    {
        InitFailures();
        InitDefects();
    }
}