using System.ComponentModel.Design;
using System.Reflection;

class ZooRepository : IListlike, IMenuable
{
    readonly List<IPen> Pens = [];

    public bool ListAllItems()
    {
        if (Pens.Count <= 0)
        {
            Console.WriteLine("No pens currently exist.");
            return false;
        }

        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Pens.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Pens[i].Describe()}");
        }

        return true;
    }

    public void AddByUserInput()
    {
        Console.WriteLine($"\nPlease enter a name for your new pen.");
        if (!ConsoleUtils.GetResponse(out string localName)) return;

        Console.WriteLine($"What type of animals would you like to store in {localName}?");
        if (!Item.SelectType(out Type? chosenSpecies) || chosenSpecies == null || !ConsoleUtils.CheckValidity($"{localName}, and {chosenSpecies.Name}")) return;

        Type genericType = typeof(Pen<>).MakeGenericType(chosenSpecies);
        IPen? pen = (IPen?) Activator.CreateInstance(genericType, localName);

        if (pen == null) return;

        Pens.Add(pen);
        Console.WriteLine($"Successfully added {localName}"!);
    }

    void AddNewPen<T>(string name) where T : Animal, new()
    {
        Pen<T> pen = new(name);
        Pens.Add(pen);
    }

    public void HandleListMenu()
    {
        if (!ListAllItems()) return;

        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select a pen from among the above choices."
        );

        int res = HandleListSelection();

        if (res != -1 && res <= Pens.Count)
        {
            HandleMenu(res);
        }
    }

    public int HandleListSelection()
    {
        bool success = ConsoleUtils.GetIntResponse(out int response);

        // If the user has chosen to exit the application, return to the main menu.
        if (!success) return -1;

        if (response <= Pens.Count)
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
        Console.WriteLine($"\nYou have selected pen {itemID}: \n{Pens[itemID - 1].Describe()}");
        Console.WriteLine("What would you like to do with this pen?");
        Console.WriteLine("1. List all animals");
        Console.WriteLine("2. Select an animal");
        Console.WriteLine("3. Add a new animal");
        Console.WriteLine("5. Delete this pen");
        Console.WriteLine("X. Return to the main menu.");

        HandleSelection(itemID);
    }

    public void HandleSelection(int itemID)
    {
        bool success = ConsoleUtils.GetIntResponse(out int response, true);

        if (!success) return;

        IPen Pen = Pens[itemID - 1];

        switch (response)
        {
            case 1:
                Pen.ListAllItems();
                break;

            case 2:
                Pen.HandleListMenu();
                break;

            case 3:
                Pen.AddByUserInput();
                break;

            case 5:
                Pens.RemoveAt(itemID - 1);
                break;

            case -1:
                return;

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }
    }
}