using System.ComponentModel.Design;

class ZooRepository : IListlike, IMenuable
{
    List<IPen> Pens = [];

    public void ListAllItems()
    {
        Console.WriteLine(""); // Just a newline for some space 
        for (int i = 0; i < Pens.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Pens[i].Describe()}");
        }
    }

    public void AddNewPen<T>(string name) where T : Animal, new()
    {
        Pen<T> pen = new(name);
        Pens.Add(pen);
    }

    public void HandleListMenu()
    {
        ListAllItems();
        Console.WriteLine(
            "\n" +
            "X. Return to the main menu." +
            "\n" +
            "Please select a pen from among the above choices."
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

        if (response <= Pens.Count + 1)
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
        Console.WriteLine($"\nYou have selected pen {itemID}.");
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
                    Pens[itemID - 1].ListAllItems();
                    break;

                case 2:
                    Pens[itemID - 1].HandleListMenu();
                    break;

                case 3:
                    Pens.RemoveAt(itemID - 1);
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