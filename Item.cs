abstract class Item
{
    public bool CheckValidity(string value)
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