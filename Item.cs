public abstract class Item(string name)
{
    public string Name { get; private set; } = name;

    public virtual void ChangeNameByInput()
    {
        Console.WriteLine($"Please input a new name for {Name}; input X to quit:");

        if (!ConsoleUtils.GetResponse(out string response) || !ConsoleUtils.CheckValidity(response))
        {
            return;
        }

        Name = response;
    }

    public static bool SelectType(out Type? chosenSpecies)
    {
        List<Type> subclasses = [.. typeof(Animal).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Animal)))
        ];

        Console.WriteLine("\nPlease select an animal type from among the following choices:");

        for (int i = 1; i <= subclasses.Count; i++)
        {
            Console.WriteLine($"{i}. {subclasses[i - 1].Name}");
        }

        if (!ConsoleUtils.GetIntResponse(out int response)
        || response >= subclasses.Count && response <= 0)
        {
            chosenSpecies = null;
            return false;
        }

        chosenSpecies = subclasses[response - 1];
        return true;
    }
}