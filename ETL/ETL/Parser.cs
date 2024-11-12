using System.Globalization;
using System.Xml.Linq;
using System.Xml.Serialization;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ETL;

public class Parser
{
    public static JObject? Parse(string data)
    {
        if (data[0] == '"')
        {
            data = data.Substring(1, data.Length - 2);
        }
        data = data.Replace("\r\n", "\n");
        object? item = TryParse(data);
        JObject? o = item != null ? (JObject)JToken.FromObject(item) : null;
        return o;
    }

    private static object? TryParse(string data)
    {
        object? item = null;
        return JsonParse(data) ?? CsvParse(data) ?? XmlParse(data);
    }
    
    public static object? JsonParse(string data)
    {
        try
        {
            if (data[0] != '{' && data[0] != '[') throw new Exception();
            return JsonConvert.DeserializeObject(data);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public static object? CsvParse(string data)
    {
        try
        { 
            using (var reader = new StringReader(data))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                object res = csv.GetRecords<object>().FirstOrDefault();
                return res;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public static object? XmlParse(string data)
    {
        try
        { 
            XDocument doc = XDocument.Parse(data);
            string json = JsonConvert.SerializeXNode(doc.Root);
            JObject temp =  JObject.Parse(json);
            return temp["root"];
        }
        catch (Exception e)
        {
            return null;
        }
    }
}