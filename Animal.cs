abstract class Animal : Item, IEats, ISpeaks
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

    public void ChangeNameByInput()
    {
        Console.WriteLine($"Please input a new name for {Name}; input X to quit:");
        bool success = ConsoleUtils.GetResponse(out string response);

        if (!CheckValidity(response) || !success)
        {
            return;
        }

        Name = response;
    }   

    public void ChangeAgeByInput()
    {
        Console.WriteLine($"Please input a new age for {Name}; input X to quit:");
        bool success = ConsoleUtils.GetIntResponse(out int response);

        if (!CheckValidity(response.ToString()) || !success)
        {
            return;
        }

        Age = response;
    }
}