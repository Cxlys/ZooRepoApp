using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

class Zoo : IMenuable
{
    readonly ZooRepository Repository = new();

    public void Run()
    {
        Console.WriteLine("\n\n\nWelcome to the Zoo app!");
        HandleMenu();
    }

    public void HandleMenu(int itemID = -1)
    {
        bool finished = false;

        while (!finished)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. List all pens");
            Console.WriteLine("2. Select a pen");
            Console.WriteLine("3. Add a new pen");
            Console.WriteLine("4. Save this zoo");
            Console.WriteLine("X. Exit the application");

            finished = HandleSelection();
        }
    }

    public bool HandleSelection(int itemID = -1)
    {
        bool success = ConsoleUtils.GetIntResponse(out int response, true);

        if (!success) return true;

        switch (response)
        {
            case 1:
                Repository.ListAllItems();
                break;

            case 2:
                Repository.HandleListMenu();
                break;

            case 3:
                Repository.AddByUserInput();
                break;

            case 4:
                if (ConsoleUtils.CheckValidity(" to save your data."))
                    SaveDataToDisk();
                break;

            case 5:
                if (ConsoleUtils.CheckValidity(" to load your data."))
                    LoadDataFromDisk();
                break;

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }

        return false;
    }

    public void SaveDataToDisk()
    {
        // Get location to save to
        Console.WriteLine("\nWhat folder would you like to save your data in? Input X to abort.");
        bool success = ConsoleUtils.GetResponse(out string location, false);

        if (!success) return;

        string path = location + "/save.txt";

        if (File.Exists(path) && !ConsoleUtils.CheckValidity(" to save your file, but a file already exists here. Overwrite? Y/N"))
            return;

        // Collect data from all pens
        List<IPen> pens = Repository.GetPenList();

        // Save data to .TXT file
        using StreamWriter sw = new StreamWriter(path, append: false);

        foreach (IPen pen in pens)
        {
            sw.WriteLine($"{pen.GetName()}");
            sw.WriteLine($"{pen.GetGenericType().Name}");
            sw.Write(pen.DescribeSeparated());
            sw.WriteLine("-");
        }

        return;
    }

    public void LoadDataFromDisk()
    {
        Console.WriteLine("\nWhere is your data stored? Input X to abort.");
        bool success = ConsoleUtils.GetResponse(out string location, false);

        if (!success) return;

        string path = location + "/save.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("\nCould not find file. Please try again.");
        }

        using StreamReader sw = new StreamReader(path);


    }

}