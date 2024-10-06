using System.Text.Json;

public static class JsonFileHandler
{
    public static List<T> ReadJsonFile<T>(string path)
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        string contents = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(contents) ?? new List<T>();
    }

    public static void WriteToJsonFile<T>(string path, List<T> contents)
    {
        string contents_s = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, contents_s);
    }
}
