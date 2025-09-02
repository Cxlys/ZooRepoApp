using System.Text.Json;

public interface IFactory
{
    public static abstract bool GenerateObject(string type, out Item? item, params object[] values);
}

public class PenFactory : IFactory
{
    public static bool GenerateObject(string type, out Item? item, params object[] values)
    {
        if (values[0] is not string)
        {
            item = null;
            return false;
        }

        string name = (string) values[0];
        string fullJsonText = (string) values[1];

        item = type.ToLower() switch
        {
            "bear" => PreparePen<Bear>(name, fullJsonText),
            // ...
            _ => null,
        };

        if (item == null) return false;

        return true;
    }

    static Pen<T> PreparePen<T>(string name, string deserializableText) where T : Animal
    {
        List<T> animals = JsonSerializer.Deserialize<List<T>>(deserializableText) ?? throw new NullReferenceException();
        Pen<T> pen = new(name, animals);

        return pen;
    }
}

public class AnimalFactory : IFactory
{
    public static bool GenerateObject(string type, out Item? item, params object[] values)
    {
        if (values[0] is not string || values[1] is not int)
        {
            item = null;
            return false;
        }

        string name = (string)values[0];
        int age = (int)values[1];

        item = type.ToLower() switch
        {
            "bear" => new Bear(name, age),
            // ...
            _ => null,
        };

        if (item == null) return false;

        return true;
    }
}