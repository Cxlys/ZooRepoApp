using System.Net;
using System.Runtime.InteropServices;

class Pen<T>(string name) : IPen where T : Animal, new()
{
    string Name = name;
    List<T> Animals { get; set; } = new();

    public string Describe()
    {
        return $"{Name}, containing {Animals.Count} animals of type {typeof(T).Name}";
    }

    public void Add(T animal)
    {
        Animals.Add(animal);
    }

    public void Remove(string name)
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
            Console.WriteLine("\nPen does not exist! Please try again.");
            return -1;
        }
    }

    public void HandleMenu(int itemID)
    {
        Console.WriteLine($"\nYou have selected animal {itemID}.");
        Console.WriteLine("What would you like to do with this pen?");
        Console.WriteLine("1. List all animals");
        Console.WriteLine("2. Select an animal");
        Console.WriteLine("3. Delete this pen");
        Console.WriteLine("X. Return to the main menu.");

        HandleSelection(itemID);
    }
    
    public void HandleSelection(int itemID)
    {
        bool check = false;
        while (!check)
        {
            Console.WriteLine("\nPlease make a selection:");
            bool success = ConsoleUtils.GetIntResponse(out int response);

            if (!success) break;

            switch (response)
            {
                case 1:
                    
                    break;

                case 2:
                    
                    break;

                case 3:
                    
                    break;

                case -1:
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
            }

            check = true;
        }
    }
}