using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

public class Pen<T>(string name) : Item(name), IPen where T : Animal
{
    readonly List<T> Animals = [];

    public string GetName() => Name;

    public Type GetGenericType()
    {
        return typeof(T);
    }

    public string Describe()
    {
        return $"{Name}, containing {Animals.Count} animals of type {typeof(T).Name}";
    }

    public string DescribeSeparated()
    {
        StringWriter buffer = new();

        foreach (T animal in Animals)
        {
            buffer.WriteLine($"{animal.Name}, {animal.Age}");
        }

        return buffer.ToString();
    }

    public void AddByUserInput()
    {
        Console.WriteLine($"\nPlease enter a name for this {typeof(T).Name}.");
        if (!ConsoleUtils.GetResponse(out string localName)) return;

        Console.WriteLine($"\nPlease enter an age for this {typeof(T).Name}");
        if (!ConsoleUtils.GetIntResponse(out int localAge)) return;

        T? Animal = (T?)Activator.CreateInstance(typeof(T), localName, localAge);
        if (Animal == null) return;

        Animals.Add(Animal);
        Console.WriteLine($"Successfully added {localName}"!);
    }

    public void RemoveByName(string name)
    {
        foreach (T animal in Animals)
        {
            if (animal.Name == name)
            {
                Animals.Remove(animal);
            }
        }
    }

    public bool ListAllItems()
    {
        if (Animals.Count <= 0)
        {
            Console.WriteLine("No animals currently exist in this pen.");
            return false;
        }

        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Animals[i].Name}, {Animals[i].Age} years old.");
        }

        return true;
    }

    public void HandleListMenu()
    {
        if (!ListAllItems()) return;

        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select an animal from among the above choices."
        );

        int res = HandleListSelection();

        if (res != -1 && res <= Animals.Count)
        {
            HandleMenu(res);
        }
    }

    public int HandleListSelection()
    {
        bool success = ConsoleUtils.GetIntResponse(out int response);

        // If the user has chosen to exit the application, return to the main menu.
        if (!success) return -1;

        if (response <= Animals.Count)
        {
            // The response is valid, and we will select a pen from the repository.
            return response;
        }
        else
        {
            Console.WriteLine("\nAnimal does not exist! Please try again.");
            return -1;
        }

        // Could also do below, but is harder to read + doesn't allow console writing
        // return (response <= Animals.Count || !success) ? response : -1;
    }

    public void HandleMenu(int itemID)
    {
        bool finished = false;

        while (!finished)
        {
            Console.WriteLine($"\nYou have selected animal {itemID}.");
            Console.WriteLine($"Name: {Animals[itemID - 1].Name}, Age: {Animals[itemID - 1].Age}");
            Console.WriteLine("What would you like to do with this animal?");
            Console.WriteLine("1. Change its name");
            Console.WriteLine("2. Change its age");
            Console.WriteLine("3. Eat!");
            Console.WriteLine("4. Speak!");
            Console.WriteLine("5. Delete this animal");
            Console.WriteLine("X. Return to pen selection.");

            finished = HandleSelection(itemID);
        }
    }

    public bool HandleSelection(int itemID)
    {
        bool success = ConsoleUtils.GetIntResponse(out int response, true);

        if (!success) return true;

        int animalIndex = itemID - 1;
        T animal = Animals[animalIndex];

        switch (response)
        {
            case 1:
                animal.ChangeNameByInput();
                break;

            case 2:
                animal.ChangeAgeByInput();
                break;

            case 3:
                animal.Eat();
                break;

            case 4:
                animal.Speak();
                break;

            case 5:
                if (ConsoleUtils.CheckValidity($"to remove {animal.Name}, of age {animal.Age}"))
                {
                    Animals.RemoveAt(animalIndex);
                    return true;
                }

                break;

            case -1:
                return true;

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }

        return false;
    }
}