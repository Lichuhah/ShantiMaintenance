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
        for (int i = 1; i < 101; i++)
        {
            using (var cmd = new NpgsqlCommand("insert into asset (name, \"key\") values (@name, @key);"))
            {
                cmd.Parameters.AddWithValue("name", "Турбина " + i);
                cmd.Parameters.AddWithValue("key", i);
                Postgres.ExecCommand(cmd);
            }
            using (var cmd = new NpgsqlCommand("insert into tech_map (name, duration, asset_id, work_type) values (@name, @duration, @asset_id, 1);"))
            {
                cmd.Parameters.AddWithValue("name", "Техническая карта для турбины  " + i);
                cmd.Parameters.AddWithValue("duration", random.Next(1, 24)); ;
                cmd.Parameters.AddWithValue("asset_id", i);
                Postgres.ExecCommand(cmd);
            }
        }
    }
}