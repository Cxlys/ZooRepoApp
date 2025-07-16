using System.Net;
using System.Security.Cryptography.X509Certificates;

static class ConsoleUtils
{
    public static bool GetIntResponse(out int response)
    {
        while (true)
        {
            Console.WriteLine("\nPlease type your value below, or \"X\" if you would like to back out:");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            else if (input.Equals("X", StringComparison.CurrentCultureIgnoreCase))
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

    public static bool GetResponse(out string response)
    {
        while (true)
        {
            Console.WriteLine("\nPlease type your value below, or \"X\" if you would like to back out:");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            else if (input.Equals("X", StringComparison.CurrentCultureIgnoreCase))
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
}