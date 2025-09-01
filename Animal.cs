using System.Diagnostics;
using System.Reflection;

public abstract class Animal(string name, int age) : Item(name), IEats, ISpeaks
{
    public int Age { get; set; } = age;

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

        if (!ConsoleUtils.GetIntResponse(out int response) || !ConsoleUtils.CheckValidity(response.ToString()))
        {
            return;
        }

        Age = response;
    }
}