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

            default:
                Console.WriteLine("Invalid input, please try again.");
                break;
        }

        return false;
    }

}