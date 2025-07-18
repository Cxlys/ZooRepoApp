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
        Console.WriteLine($"Please input a new age for {Name}; input X to quit:");

        if (!ConsoleUtils.GetIntResponse(out int response) || !CheckValidity(response.ToString()))
        {
            return;
        }

        Age = response;
    }

    // TODO: Foreach type in the subclasses, print and let user select one
    public static bool SelectType(out Type chosenSpecies)
    {
        List<Type> subclasses = [.. typeof(Animal).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Animal)))
        ];

        foreach (Type type in subclasses)
        {

        }

        chosenSpecies = null;
        return false;
    }
}