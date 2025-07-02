class Zoo : IMenuable
{
    readonly ZooRepository Repository = new();
    bool Running;

    public void Run()
    {
        Console.WriteLine("\n\n\nWelcome to the Zoo app!");
        while (Running)
        {
            HandleMenu();
        }
    }

    public void HandleMenu()
    {
        Console.WriteLine("\nPlease select an option:");
        Console.WriteLine("1. List all pens");
        Console.WriteLine("2. Select a pen");
        Console.WriteLine("X. Exit the application");

        HandleSelection();
    }

    public void HandleSelection()
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
                    Repository.ListAllItems();
                    break;

                case 2:
                    Repository.HandleMenu();
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
            }

            check = true;
        }
    }

}