// See https://aka.ms/new-console-template for more information

using System.Globalization;
using Cassandra;
using DatabasePrepare;
using Npgsql;
string path = @"C:\Users\lichuha\Desktop\Диплом датасет\";
void CassandaraInit()
{
   
}

string connectionString = "";

void ExecCommand(NpgsqlCommand command)
{
    try
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            command.Connection = conn;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

void TruncateTable(string table)
{
    try
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("TRUNCATE TABLE " + table + " RESTART IDENTITY CASCADE" , conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

void ClearDB()
{
    connectionString = "host=localhost;port=5431;database=bdo;user id=postgres;password=postgres";
    TruncateTable("Asset");
    connectionString = "host=localhost;port=5431;database=maint;user id=postgres;password=postgres";
    ExecCommand(new NpgsqlCommand("update public.maint set status_id = null where id > 0;"));
    TruncateTable("Maint_Status");
    TruncateTable("Maint");
    TruncateTable("Tech_Map");
    ExecCommand(new NpgsqlCommand("update public.defect set status_id = null where id > 0;"));
    TruncateTable("Defect_Status");
    TruncateTable("Defect");
}

void CreateAssets()
{
    //clear assets
    //create table
    //insert into table
    //clear techmaps
    //create table
    //create default tech maps
    Random random = new Random(DateTime.UtcNow.Microsecond);
    for (int i = 1; i < 101; i++)
    {
        connectionString = "host=localhost;port=5431;database=bdo;user id=postgres;password=postgres";
        using (var cmd = new NpgsqlCommand("insert into asset (name, \"key\") values (@name, @key);"))
        {
            cmd.Parameters.AddWithValue("name", "Турбина " + i);
            cmd.Parameters.AddWithValue("key", i);
            ExecCommand(cmd);
        }
        connectionString = "host=localhost;port=5431;database=maint;user id=postgres;password=postgres";
        using (var cmd = new NpgsqlCommand("insert into tech_map (name, hours, staff_need, asset_id, by_default) values (@name, @hours, @staffneed, @asset_id, true);"))
        {
            cmd.Parameters.AddWithValue("name", "Техническая карта для турбины  " + i);
            cmd.Parameters.AddWithValue("hours", random.Next(1, 24));
            cmd.Parameters.AddWithValue("staffneed", random.Next(1, 10));
            cmd.Parameters.AddWithValue("asset_id", i);
            ExecCommand(cmd);
        }
    }
}

void CreateWorks()
{
    connectionString = "host=localhost;port=5431;database=maint;user id=postgres;password=postgres";
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
            
            using (var cmd = new NpgsqlCommand("insert into public.maint (datetime, name, asset_id, asset_key) values (@d, @n, @a, ' ');"))
            {
                cmd.Parameters.AddWithValue("d", time);
                cmd.Parameters.AddWithValue("n", "Обслуживание турбины " + assetId + " от " + time.ToShortDateString()); 
                cmd.Parameters.AddWithValue("a", assetId);
                ExecCommand(cmd);
            }
            
            using (var cmd = new NpgsqlCommand("insert into maint_status (status, start, maint_id) values (3, @d, @i);"))
            {
                cmd.Parameters.AddWithValue("d", time);
                cmd.Parameters.AddWithValue("i", id);
                ExecCommand(cmd);
            }

            using (var cmd = new NpgsqlCommand("update public.maint set status_id = @i where id = @i;"))
            {
                cmd.Parameters.AddWithValue("i", id);
                ExecCommand(cmd);
            }
            
            id++;
        }
    }
}

void CreateDefects()
{
    connectionString = "host=localhost;port=5431;database=maint;user id=postgres;password=postgres";
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
            
            using (var cmd = new NpgsqlCommand("insert into public.defect (datetime, name, asset_id,asset_key, type) values (@d, @n, @a, ' ', 1);"))
            {
                cmd.Parameters.AddWithValue("d", time);
                cmd.Parameters.AddWithValue("n", "Дефект турбины " + assetId + " от " + time.ToShortDateString()); 
                cmd.Parameters.AddWithValue("a", assetId);
                ExecCommand(cmd);
            }
            
            using (var cmd = new NpgsqlCommand("insert into defect_status (status, start, defect_id) values (3, @d, @i);"))
            {
                cmd.Parameters.AddWithValue("d", time);
                cmd.Parameters.AddWithValue("i", id);
                ExecCommand(cmd);
            }

            using (var cmd = new NpgsqlCommand("update public.defect set status_id = @i where id = @i;"))
            {
                cmd.Parameters.AddWithValue("i", id);
                ExecCommand(cmd);
            }
            
            id++;
        }
    }
}


//CassandaraInit();
//Telemetry.Init();
Assets.Clear();
Defect.Clear();
Works.Clear();
Assets.Init();
Defect.Init();
Works.Init();