using Npgsql;

namespace DatabasePrepare;

public static class Postgres
{
    private static string connectionString = "host=localhost;port=5431;database=shanti;user id=postgres;password=postgres";
        
    public static NpgsqlConnection GetSession()
    {
        return new NpgsqlConnection(connectionString);
    }

    public static void Truncate(string table)
    {
        try
        {
            using (var conn = GetSession())
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
    
    public static void ExecCommand(NpgsqlCommand command)
    {
        try
        {
            using (var conn = GetSession())
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

    public static string GetReader(NpgsqlCommand command)
    {
        try
        {
            using (var conn = GetSession())
            {
                command.Connection = conn;
                conn.Open();
                var reader  = command.ExecuteReader();
                if (reader.Read())
                    return reader[0].ToString();
                else return "";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}