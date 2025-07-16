abstract class Item(string name)
{
    public string Name { get; private set; } = name;

    public virtual void ChangeNameByInput()
    {
        Console.WriteLine($"Please input a new name for {Name}; input X to quit:");

        if (!ConsoleUtils.GetResponse(out string response) || !CheckValidity(response))
        {
            return;
        }

        Name = response;
    }

    protected static bool CheckValidity(string value)
    {
        Console.WriteLine($"\nYou have selected {value}. Would you like to continue? Y/N");

        while (true)
        {
            string? input = Console.ReadLine();

            if (input != null)
            {
                switch (input.ToUpper())
                {
                    case "Y":
                        return true;

                    case "N":
                        return false;

                    default:
                        break;
                }
            }

            Console.WriteLine("Invalid input, please try again.");
        }
    }
}