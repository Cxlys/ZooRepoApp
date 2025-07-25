using System.Diagnostics;
using System.Reflection;

abstract class Animal(string name, int age) : Item(name), IEats, ISpeaks
{
    public int Age = age;

    public void Eat()
    {
        Console.WriteLine("Generic animal eating sound!");
    }

    public void Speak()
    {
        Console.WriteLine("Generic animal speaking sound!");
    }

    public void ChangeAgeByInput()
    {
        Console.WriteLine($"Please input a new age for {Name}:");

        if (!ConsoleUtils.GetIntResponse(out int response) || !CheckValidity(response.ToString()))
        {
            return;
        }

        Age = response;
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
        || response <= subclasses.Count
        || !CheckValidity(response.ToString()))
        {
            Console.WriteLine("Invalid input, please try again.");
            chosenSpecies = null;
            return false;
        }

        chosenSpecies = subclasses[response - 1];
        return true;
    }
}