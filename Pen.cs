using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

class Pen<T>(string name) : Item(name), IPen where T : Animal, new()
{
    readonly List<T> Animals = [];

    public string Describe()
    {
        return $"{Name}, containing {Animals.Count} animals of type {typeof(T).Name}";
    }

    public void AddByUserInput()
    {
        Console.WriteLine($"\nPlease enter a name for this {typeof(T)}.");
        if (!ConsoleUtils.GetResponse(out string localName)) return;

        Console.WriteLine($"\nPlease enter an age for this {typeof(T)}.");
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

    public void ListAllItems()
    {
        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Animals[i].Name}, {Animals[i].Age} years old.");
        }
    }

    public void HandleListMenu()
    {
        ListAllItems();

        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select an animal from among the above choices."
        );

        int res = HandleListSelection();

        if (res != -1)
        {
            HandleMenu(res);
        }
    }

    public int HandleListSelection()
    {
        bool success = ConsoleUtils.GetIntResponse(out int response);

        // If the user has chosen to exit the application, return to the main menu.
        if (!success) return -1;

        if (response <= Animals.Count + 1)
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
        // return (response <= Animals.Count + 1 || !success) ? response : -1;
    }

    public void HandleMenu(int itemID)
    {
        Console.WriteLine($"\nYou have selected animal {itemID}.");
        Console.WriteLine($"Name: {Animals[itemID - 1].Name}, Age: {Animals[itemID - 1].Age}");
        Console.WriteLine("What would you like to do with this animal?");
        Console.WriteLine("1. Change its name");
        Console.WriteLine("2. Change its age");
        Console.WriteLine("3. Eat!");
        Console.WriteLine("4. Speak!");
        Console.WriteLine("5. Delete this animal");
        Console.WriteLine("X. Return to the main menu.");

        HandleSelection(itemID);
    }
    
    public void HandleSelection(int itemID)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nPlease make a selection:");
            bool success = ConsoleUtils.GetIntResponse(out int response);

            if (!success) break;

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
                    Animals.RemoveAt(animalIndex);
                    break;

                case -1:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
            }
        }
    }
}