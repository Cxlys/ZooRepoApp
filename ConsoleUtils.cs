using System.Net;
using System.Security.Cryptography.X509Certificates;

static class ConsoleUtils
{
    public static bool GetIntResponse(out int response)
    {
        while (true)
        {
            Console.WriteLine("\nPlease type your value below:");
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
}