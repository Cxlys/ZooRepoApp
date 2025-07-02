abstract class Animal : IEats, ISpeaks, IMenuable
{
    public string Name;
    public int Age;

    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Eat() { }
    public void Speak() { }

    public void HandleMenu()
    {
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("1. Change name");
        Console.WriteLine("2. Change age");
        Console.WriteLine("X. Return to main menu.");

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
                    break;

                case 2:
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
            }

            check = true;
        }
    }
}