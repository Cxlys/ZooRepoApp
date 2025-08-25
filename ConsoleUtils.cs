using System.Net;
using System.Security.Cryptography.X509Certificates;

static class ConsoleUtils
{
    public static bool GetIntResponse(out int response, bool showStatusText = false)
    {
        while (true)
        {
            if (showStatusText) Console.WriteLine("\nPlease type your value below, or \"X\" if you would like to back out:");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            else if (input.ToUpper() == "X")
            {
                response = -1;
                return false;
            }
            else
            {
                if (int.TryParse(input, out response) && response >= 0)
                {
                    return true;
                }

                Console.WriteLine("Failed to parse response, please try again.");
            }
        }
    }

    public static bool GetResponse(out string response, bool showStatusText = false)
    {
        while (true)
        {
            if (showStatusText) Console.WriteLine("\nPlease type your value below, or \"X\" if you would like to back out:");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            else if (input.ToUpper() == "X")
            {
                response = "";
                return false;
            }
            else
            {
                response = input;
                return true;
            }
        }
    }

    public static bool CheckValidity(string value)
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