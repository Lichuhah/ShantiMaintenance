using Npgsql;

namespace DatabasePrepare;

public static class Assets
{
    public static void Clear()
    {
        Postgres.Truncate("tech_map");
        Postgres.Truncate("asset");
    }

    public static void Init()
    {
        Random random = new Random(DateTime.UtcNow.Microsecond);
        for (int i = 1; i < 5; i++)
        {
            using (var cmd = new NpgsqlCommand("insert into asset_type (name) values (@name);"))
            {
                cmd.Parameters.AddWithValue("name", "Тип турбины " + i);
                Postgres.ExecCommand(cmd);
            }
        }
        
        string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
        int k = 1;
        using (var reader = new StreamReader(path + "PdM_machines.csv"))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                int type = Int32.Parse(values[1][6].ToString());
                int age = Int32.Parse(values[2]);
                DateTime mandate = DateTime.UtcNow.AddYears(-1 * age);
                using (var cmd = new NpgsqlCommand("insert into asset (name, \"key\", type_id, manufacture_date) values (@name, @key, @t, @m);"))
                {
                    cmd.Parameters.AddWithValue("name", "Турбина " + k);
                    cmd.Parameters.AddWithValue("key", k);
                    cmd.Parameters.AddWithValue("t", type);
                    cmd.Parameters.AddWithValue("m", mandate);
                    Postgres.ExecCommand(cmd);
                }
                using (var cmd = new NpgsqlCommand("insert into tech_map (name, duration, asset_id, work_type) values (@name, @duration, @asset_id, 1);"))
                {
                    cmd.Parameters.AddWithValue("name", "Техническая карта для турбины  " + k);
                    cmd.Parameters.AddWithValue("duration", random.Next(1, 24)); ;
                    cmd.Parameters.AddWithValue("asset_id", k);
                    Postgres.ExecCommand(cmd);
                }

                k++;
            }
        }
    }
}