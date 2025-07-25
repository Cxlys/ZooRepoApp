class Zoo : IMenuable
{
    readonly ZooRepository Repository = new();
    bool Running = true;

    public void Run()
    {
        Console.WriteLine("\n\n\nWelcome to the Zoo app!");
        while (Running)
        {
            HandleMenu();
        }
    }

    public void HandleMenu(int itemID = -1)
    {
        Console.WriteLine("\nPlease select an option:");
        Console.WriteLine("1. List all pens");
        Console.WriteLine("2. Select a pen");
        Console.WriteLine("3. Add a new pen");
        Console.WriteLine("X. Exit the application");

        HandleSelection();
    }

    public void HandleSelection(int itemID = -1)
    {
        Console.WriteLine("\nPlease make a selection:");
        bool success = ConsoleUtils.GetIntResponse(out int response);

        if (!success) return;

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

            case -1:
                Running = false;
                break;

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }
    }

}